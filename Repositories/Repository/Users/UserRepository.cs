using Dapper;
using Data;
using Entities;
using Microsoft.Extensions.Configuration;
using Repositories.Repository;
using System.Data;
using Transversals;
using Transversals.Filters;

namespace Repositories
{
    public class UserRepository : GenerycRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Create or update root parameters for hangfire job
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public async Task<Root> CreateUpdateRootParameters(Root root)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumRootParams.Page), root.page);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumRootParams.PerPage), root.per_page);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumRootParams.Total), root.total);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumRootParams.TotalPages), root.total_pages);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumRootParams.Updated), root.updated);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumRootParams.ActualPage), root.actual_page);
            return await GetAsyncFirst<Root>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), dynamicParameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Creates a new User in db
        /// </summary>
        /// <returns></returns>
        public async Task<Datum> CreateUser(Datum user)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Avatar), user.avatar);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.FirstName), user.first_name);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Email), user.email);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.LastName), user.last_name);
            return await GetAsyncFirst<Datum>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), dynamicParameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Generates credentials for user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DatumLogin> GenerateCredentials(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Id), id);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Username), Guid.NewGuid());
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Password), Guid.NewGuid());
            return await GetAsyncFirst<DatumLogin>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), dynamicParameters, CommandType.StoredProcedure);
        }


        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Datum>> GetAllUsers()
        {
            return await GetAsyncList<Datum>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gest all users with credentials
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DatumLogin>> GetAllUsersWithLogin()
        {
            return await GetAsyncList<DatumLogin>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets root parameters for hangfire job
        /// </summary>
        /// <returns></returns>
        public async Task<Root> GetRootParameters()
        {
            return await GetAsyncFirst<Root>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets an User by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Datum> GetUserById(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Id), id);
            return await GetAsyncFirst<Datum>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), dynamicParameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// gets users from db
        /// </summary>
        /// <returns>List of Users</returns>
        public async Task<IEnumerable<Datum>> GetUsers(PaginationFilter filter)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumPagFilterParams.PageSize), filter.PageSize);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumPagFilterParams.PageNumber), filter.PageNumber);
            return await GetAsyncList<Datum>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), dynamicParameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Update an existing User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Datum> UpdateUser(Datum user)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Id), user.id);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Avatar), user.avatar);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.FirstName), user.first_name);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.Email), user.email);
            dynamicParameters.Add(EnumsHelper.GetEnumDescription(EnumUserParams.LastName), user.last_name);
            return await GetAsyncFirst<Datum>(HelperDBParameters.BuilderFunction(
                HelperDBParameters.EnumSchemas.DBO), dynamicParameters, CommandType.StoredProcedure);
        }
    }
}