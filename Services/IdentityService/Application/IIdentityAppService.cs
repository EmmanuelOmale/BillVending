using Application.Dtos;

namespace Application
{
    public interface IIdentityAppService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}