namespace AnaliticTrend.Application.Services
{
    public interface IIdentityService
    {
        Task<bool> IsValidToken(string token);
    }
}
