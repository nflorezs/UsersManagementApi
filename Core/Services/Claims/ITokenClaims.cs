using Dto;

namespace Services
{
    public interface ITokenClaims
    {
        Task<string> GetTokenAsync(DatumLoginDto user);
    }
}