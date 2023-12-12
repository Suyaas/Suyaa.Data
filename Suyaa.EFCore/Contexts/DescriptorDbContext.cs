using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using sy;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// 带描述的数据库上下文
    /// </summary>
    public abstract class DescriptorDbContext : BuilderDbContext
    {
        private readonly IEnumerable<IEntityModelConvention> _conventions;
        private readonly IEntityModelConventionFactory _entityModelConventionFactory;

        /// <summary>
        /// 带描述的数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="entityModelConventionFactory"></param>
        /// <param name="dbContextOptionsBuilderProvider"></param>
        public DescriptorDbContext(
            IEntityModelConventionFactory entityModelConventionFactory,
            IDbContextOptionsBuilderProvider dbContextOptionsBuilderProvider,
            IDbConnectionDescriptor descriptor)
            : base(
                  descriptor.DatabaseType.GetEfCoreProvider().DbContextOptionsProvider,
                  dbContextOptionsBuilderProvider,
                  descriptor.ToConnectionString())
        {
            ConnectionDescriptor = descriptor;
            _entityModelConventionFactory = entityModelConventionFactory;
            _conventions = _entityModelConventionFactory.Conventions;
        }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 创建模型构建器
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in entities)
            {
                modelBuilder.Entity(entity.Name, builder =>
                {
                    // 处理表名
                    var dbEntityModel = new DbEntityModel(entity.ClrType);
                    foreach (var convention in _conventions)
                    {
                        convention.OnEntityModeling(dbEntityModel);
                    }
                    builder.ToTable(dbEntityModel.Name, dbEntityModel.Schema);
                    // 处理表字段
                    var properties = entity.GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyInfo is null) continue;
                        var fieldModel = new FieldModel(0, property.PropertyInfo);
                        dbEntityModel.AddField(fieldModel);
                        foreach (var convention in _conventions)
                        {
                            convention.OnPropertyModeling(dbEntityModel, fieldModel);
                        }
                        builder.Property(property.Name).HasColumnName(fieldModel.Name);
                        builder.Property(property.Name).HasComment(fieldModel.Description);
                    }
                });
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
