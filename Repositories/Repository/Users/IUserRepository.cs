using Entities;
using Transversals.Filters;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Datum>> GetUsers(PaginationFilter filter);
        Task<Datum> CreateUser(Datum user);
        Task<IEnumerable<DatumLogin>> GetAllUsersWithLogin();
        Task<Datum> GetUserById(int id);
        Task<DatumLogin> GenerateCredentials(int id);
        Task<Root> GetRootParameters();
        Task<IEnumerable<Datum>> GetAllUsers();
        Task<Root> CreateUpdateRootParameters(Root root);
        Task<Datum> UpdateUser(Datum request);
    }
}