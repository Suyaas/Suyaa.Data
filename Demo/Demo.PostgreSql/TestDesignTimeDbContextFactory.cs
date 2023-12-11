using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.Data.Providers;
using Suyaa.EFCore.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PostgreSql
{
    public sealed class TestDesignTimeDbContextFactory : DesignTimeDbContextFactory<TestDbContext>
    {

        public TestDesignTimeDbContextFactory() : base(new TestDbConnectionDescriptorFactory())
        {
            Console.WriteLine("TestDesignTimeDbContextFactory");
        }

        public override TestDbContext CreateDbContext(IDbConnectionDescriptorFactory dbConnectionDescriptorFactory, string[] args)
        {
            Console.WriteLine(dbConnectionDescriptorFactory.DefaultConnection.ToConnectionString());
            return new TestDbContext(dbConnectionDescriptorFactory.DefaultConnection, new EntityModelConventionFactory(Enumerable.Empty<IEntityModelConvention>()));
        }
    }
}
