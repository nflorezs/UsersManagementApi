using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public abstract class GenerycRepository
    {
        /// <summary>
        /// App configuration variables
        /// </summary>
        private readonly IConfiguration _configuration;

        protected GenerycRepository(IConfiguration configuration) => this._configuration = configuration;

        private SqlConnection GetConnection(string dbConnection) => new SqlConnection(dbConnection);

        /// <summary>
        /// Gets the first element returned from query
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="NameProcedureOrQueryString"></param>
        /// <param name="parameters"></param>
        /// <param name="typeCommand"></param>
        /// <returns></returns>
        public async Task<TOutput> GetAsyncFirst<TOutput>(
            string NameProcedureOrQueryString, DynamicParameters parameters, CommandType typeCommand) where TOutput : new()
        {
            using var connection = GetConnection(this._configuration.GetConnectionString("connectionName"));
            var cmd = new CommandDefinition(NameProcedureOrQueryString, null, null, null, typeCommand);
            await connection.OpenAsync();
            if (parameters != null)
                cmd = new CommandDefinition(NameProcedureOrQueryString, parameters, null, null, typeCommand);
            var retorno = await connection.QueryFirstOrDefaultAsync<TOutput>(cmd);
            return retorno;
        }

        /// <summary>
        /// Gets a list returned from query
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="NameProcedureOrQueryString"></param>
        /// <param name="parameters"></param>
        /// <param name="typeCommand"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TOutput>> GetAsyncList<TOutput>(string NameProcedureOrQueryString, DynamicParameters? parameters, CommandType typeCommand) where TOutput : new()
        {
            using var connection = GetConnection(this._configuration.GetConnectionString("connectionName"));
            var cmd = new CommandDefinition(NameProcedureOrQueryString, parameters, null, null, typeCommand);
            await connection.OpenAsync();
            if (parameters != null)
                cmd = new CommandDefinition(NameProcedureOrQueryString, parameters, null, null, typeCommand);
            return await connection.QueryAsync<TOutput>(cmd);
        }
    }
}
