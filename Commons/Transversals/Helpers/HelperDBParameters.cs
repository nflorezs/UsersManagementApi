using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Transversals.EnumsHelper;

namespace Transversals
{
    public class HelperDBParameters
    {
        /// <summary>
        /// New SQL parameter
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlParameter NewParameter(string ParameterName, object value, SqlDbType type)
        {
            return new SqlParameter
            {
                ParameterName = ParameterName,
                SqlValue = value ?? DBNull.Value,
                SqlDbType = type
            };
        }

        /// <summary>
        /// Schema SQL
        /// </summary>
        /// <param name="squema"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string BuilderFunction(EnumSchemas squema, [System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return squema switch
                {
                    EnumSchemas.DBO => $"{GetEnumDescription(EnumSchemas.DBO)}.{name}",
                    EnumSchemas.SETTING => $"{GetEnumDescription(EnumSchemas.SETTING)}.{name}",
                    EnumSchemas.SECURITY => $"{GetEnumDescription(EnumSchemas.SECURITY)}.{name}",

                    _ => name,
                };
            }
            else
                return name;
        }

        /// <summary>
        /// Schemas
        /// </summary>
        public enum EnumSchemas
        {
            [Description("dbo")]
            DBO,

            [Description("setting")]
            SETTING,

            [Description("security")]
            SECURITY
        }
    }
}
