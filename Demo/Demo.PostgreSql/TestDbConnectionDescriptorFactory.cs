using Suyaa.Data;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Kernel.Enums;
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

        public IDbConnectionDescriptor GetConnection(string name)
        {
            throw new NotImplementedException();
        }

        public IDbConnectionDescriptor GetDefaultConnection()
            => new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, "server=10.10.100.11;port=5432;database=yan_xin;username=dbadmin;password=123456");
    }
}
