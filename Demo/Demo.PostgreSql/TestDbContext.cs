using Demo.PostgreSql.Entities;
using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Models.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.PostgreSQL;

namespace Demo.PostgreSql
{
    public class TestDbContext : DefineDbContext
    {
        public DbSet<People> Peoples { get; set; }

        public TestDbContext(IDbConnectionDescriptor descriptor, IEntityModelConventionFactory entityModelConventionFactory)
            : base(entityModelConventionFactory, descriptor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<People>().HasIndex(p => p.Name);
        }
    }
}
