using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.EFCore.Contexts;
using SuyaaTest.PostgreSQL.Entities;
using SuyaaTest.PostgreSQL.ModelConventions;

namespace SuyaaTest.PostgreSQL
{
    public class TestDbContext : DefineDbContext
    {
        public DbSet<Test> Tests { get; set; }

        public TestDbContext(IDbConnectionDescriptor descriptor) : base(new EntityModelConventionFactory(new List<IEntityModelConvention> { new LowercaseUnderlinedModelConvention() }), descriptor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>().HasIndex(p => p.Content);
        }
    }
}
