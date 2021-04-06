using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StudentTutorApi.Core.DataAccess.Internal
{
    internal class SqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            this._config = config;
        }
        private string GetConnectionString(string name)
        {
           // var data = _config.GetConnectionString(name);
            return _config.GetConnectionString(name); //ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameter, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameter, commandType: CommandType.StoredProcedure).AsList();

                return rows;
            }
        }
        public void SaveData<T>(string storedProcedure, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
