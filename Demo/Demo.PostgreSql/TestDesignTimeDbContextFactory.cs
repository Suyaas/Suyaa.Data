using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.EFCore.Factories;

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
            Console.WriteLine(dbConnectionDescriptorFactory.GetDefaultConnection().ToConnectionString());
            return new TestDbContext(dbConnectionDescriptorFactory.GetDefaultConnection(), new EntityModelConventionFactory(Enumerable.Empty<IEntityModelConvention>()));
        }
    }
}
