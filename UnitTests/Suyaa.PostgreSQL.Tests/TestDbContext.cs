using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.PostgreSQL;
using Suyaa.Sqlite.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.PostgreSQL
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
