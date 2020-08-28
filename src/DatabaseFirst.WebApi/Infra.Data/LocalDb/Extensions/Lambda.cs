using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Extensions
{
    public static class Lambda
    {
        public static List<T> RawSqlQuery<T>(this ContextEf context, string query, SqlParameter[] parameters,
            Func<DbDataReader, T> map)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                        entities.Add(map(result));

                    return entities;
                }
            }
        }
    }
}