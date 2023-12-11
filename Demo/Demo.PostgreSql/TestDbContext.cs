using Demo.PostgreSql.Entities;
using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.PostgreSQL;

namespace Demo.PostgreSql
{
    public class TestDbContext : DescriptorDbContext
    {
        public DbSet<People> Peoples { get; set; }

        public TestDbContext(IDbConnectionDescriptor descriptor, IEntityModelConventionFactory entityModelConventionFactory)
            : base(descriptor, entityModelConventionFactory)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<People>().HasIndex(p => p.Name);
        }
    }
}
