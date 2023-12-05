using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PostgreSql
{
    public sealed class TestDbConnectionDescriptorFactory : IDbConnectionDescriptorFactory
    {
        public TestDbConnectionDescriptorFactory()
        {
            Console.WriteLine("TestDbConnectionDescriptorFactory");
        }

        public IDbConnectionDescriptor DefaultConnection => new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, "server=10.10.100.11;port=5432;database=yan_xin;username=dbadmin;password=123456");

        public IDbConnectionDescriptor GetConnection(string name)
        {
            throw new NotImplementedException();
        }
    }
}
