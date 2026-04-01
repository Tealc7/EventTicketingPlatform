using EventTicketingPlatform.Application.DTOs.Auth;
using EventTicketingPlatform.Application.Interfaces;
using EventTicketingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using EventTicketingPlatform.Infrastructure.Persistence;

namespace EventTicketingPlatform.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(AppDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email and password are required"
                };
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email);
            
            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            if (!user.IsActive)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "User account is inactive"
                };
            }

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                User = new UserAuthDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role.Name
                }
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email and password are required"
                };
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Passwords do not match"
                };
            }

            if (request.Password.Length < 8)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Password must be at least 8 characters long"
                };
            }

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email is already registered"
                };
            }

            var customerRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == "Customer");

            if (customerRole == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Customer role not found"
                };
            }

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = customerRole.Id,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users
           .Include(u => u.Role)
           .FirstOrDefaultAsync(u => u.Id == user.Id);

            var token = _jwtTokenService.GenerateToken(user!);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Registration successful",
                Token = token,
                User = new UserAuthDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role.Name
                }
            };
        }
    }
}
