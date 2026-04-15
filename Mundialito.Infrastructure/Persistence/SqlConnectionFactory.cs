using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mundialito.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mundialito.Infrastructure.Persistence
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
