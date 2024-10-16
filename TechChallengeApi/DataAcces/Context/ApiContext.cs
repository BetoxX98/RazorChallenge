﻿using Domain.Entities;
using Domain.Entities.Base;
using Infrastructure.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Context
{
    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        #region SaveChanges
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnAdd();
            OnUpdate();
            OnDelete();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnAdd();
            OnUpdate();
            OnDelete();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion SaveChanges

        #region Triggers
        private void OnAdd()
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity<Guid> && x.State == EntityState.Added))
            {
                entity.Property(nameof(BaseEntity<Guid>.CreationDate)).CurrentValue = DateTime.UtcNow;
                entity.Property(nameof(BaseEntity<Guid>.CreationDate)).IsModified = true;
                entity.Property(nameof(BaseEntity<Guid>.UpdateDate)).CurrentValue = DateTime.UtcNow;
                entity.Property(nameof(BaseEntity<Guid>.UpdateDate)).IsModified = true;
            }
        }

        private void OnUpdate()
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity<Guid> && x.State == EntityState.Modified))
            {
                entity.Property(nameof(BaseEntity<Guid>.UpdateDate)).CurrentValue = DateTime.UtcNow;
                entity.Property(nameof(BaseEntity<Guid>.UpdateDate)).IsModified = true;
            }
        }

        private void OnDelete()
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity is SoftDeleteBaseEntity<Guid> && x.State == EntityState.Deleted))
            {
                entity.Property(nameof(SoftDeleteBaseEntity<Guid>.IsDeleted)).CurrentValue = true;
                entity.Property(nameof(SoftDeleteBaseEntity<Guid>.IsDeleted)).IsModified = true;
                entity.Property(nameof(BaseEntity<Guid>.UpdateDate)).CurrentValue = DateTime.UtcNow;
                entity.Property(nameof(BaseEntity<Guid>.UpdateDate)).IsModified = true;
                entity.State = EntityState.Modified;
            }
        }
        #endregion
    }
}
