using Demo.PostgreSql.Entities;
using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.PostgreSQL;

namespace Demo.PostgreSql
{
    public class TestDbContext : PostgreSqlContext
    {
        public DbSet<People> Peoples { get; set; }

        public TestDbContext(IDbConnectionDescriptor descriptor) : base(descriptor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<People>().HasIndex(p => p.Name);
        }
    }
}
