using Dto;
using Transversals;
using Transversals.Filters;

namespace Services
{
    public interface IUserService
    {
        Task<Response<IEnumerable<DatumDto>>> GetUsers(PaginationFilter filter);
        Task<Response<IEnumerable<DatumDto>>> GetUsersFromExternalApi();
        Task<Response<DatumDto>> GetUserById(int id);
        Task<Response<DatumDto>> CreateUser(DatumDto userData);
        Task<Response<DatumDto>> UpdateUser(DatumDto userData);
        Task<Response<AuthenticateResponseDto>> Authenticate(AuthenticateRequestDto model);
        Task<Response<DatumLoginDto>> GenerateCredentials(int id);
    }
}