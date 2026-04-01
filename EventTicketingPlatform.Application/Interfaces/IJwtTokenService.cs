
using EventTicketingPlatform.Domain.Entities;

namespace EventTicketingPlatform.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
