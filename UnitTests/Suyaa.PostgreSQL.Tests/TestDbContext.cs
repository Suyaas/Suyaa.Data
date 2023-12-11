using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Contexts;
using SuyaaTest.PostgreSQL.Entities;

namespace SuyaaTest.PostgreSQL
{
    public class TestDbContext : DescriptorDbContext
    {
        public DbSet<Test> Tests { get; set; }

        public TestDbContext(IDbConnectionDescriptor descriptor) : base(descriptor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>().HasIndex(p => p.Content);
        }
    }
}
