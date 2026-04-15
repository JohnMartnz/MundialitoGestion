using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mundialito.Application.Common.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
