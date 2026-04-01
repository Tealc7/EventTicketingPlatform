
using EventTicketingPlatform.Application.DTOs.Auth;

namespace EventTicketingPlatform.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
