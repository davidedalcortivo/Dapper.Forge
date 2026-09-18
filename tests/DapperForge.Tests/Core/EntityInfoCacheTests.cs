using DapperForge.Core.Caching;
using DapperForge.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace DapperForge.Tests.Core
{
    /// <summary>
    /// Exercises the entity-mapping rules enforced by <see cref="EntityInfoCache{TEntity}"/>'s static constructor.
    /// Every failure scenario uses its own private entity type: the cache is keyed per closed generic type, and once
    /// a type's static constructor throws, every later access to that same type re-throws
    /// <see cref="TypeInitializationException"/> rather than the original exception, so scenarios must not share a type.
    /// </summary>
    public class EntityInfoCacheTests
    {
        private sealed class EntityWithIdByName
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void EntityWithIdProperty_UsesItAsIdentifierWithoutKeyAttribute()
        {
            Assert.Equal("Id", EntityInfoCache<EntityWithIdByName>.IdProperty.Name);
            Assert.Equal(nameof(EntityWithIdByName), EntityInfoCache<EntityWithIdByName>.TableName);
        }

        private sealed class EntityWithExplicitKey
        {
            [Key]
            public Guid Code { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void EntityWithKeyAttribute_UsesItAsIdentifierRegardlessOfName()
        {
            Assert.Equal("Code", EntityInfoCache<EntityWithExplicitKey>.IdProperty.Name);
        }

        private sealed class EntityWithMultipleKeys
        {
            [Key]
            public int Id { get; set; }

            [Key]
            public int OtherId { get; set; }
        }

        [Fact]
        public void EntityWithMultipleKeyAttributes_ThrowsOnFirstAccess()
        {
            TypeInitializationException ex = Assert.Throws<TypeInitializationException>(
                () => _ = EntityInfoCache<EntityWithMultipleKeys>.IdProperty);

            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        private sealed class EntityWithoutIdOrKey
        {
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void EntityWithoutIdOrKeyAttribute_ThrowsOnFirstAccess()
        {
            TypeInitializationException ex = Assert.Throws<TypeInitializationException>(
                () => _ = EntityInfoCache<EntityWithoutIdOrKey>.IdProperty);

            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        private sealed class EntityWithReservedPropertyName
        {
            public int Id { get; set; }

#pragma warning disable IDE1006
            public string p0 { get; set; } = string.Empty;
#pragma warning restore IDE1006
        }

        [Fact]
        public void EntityWithReservedParameterName_ThrowsOnFirstAccess()
        {
            TypeInitializationException ex = Assert.Throws<TypeInitializationException>(
                () => _ = EntityInfoCache<EntityWithReservedPropertyName>.IdProperty);

            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Table("Widgets", Schema = "inventory")]
        private sealed class EntityWithTableAttribute
        {
            public int Id { get; set; }
        }

        [Fact]
        public void EntityWithTableAttribute_OverridesTableAndSchemaName()
        {
            Assert.Equal("Widgets", EntityInfoCache<EntityWithTableAttribute>.TableName);
            Assert.Equal("inventory", EntityInfoCache<EntityWithTableAttribute>.SchemaName);
        }

        private sealed class EntityWithInvalidColumnName
        {
            public int Id { get; set; }

            [Column("bad name")]
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void InvalidColumnName_ThrowsOnFirstAccess()
        {
            TypeInitializationException ex = Assert.Throws<TypeInitializationException>(
                () => _ = EntityInfoCache<EntityWithInvalidColumnName>.IdProperty);

            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        private sealed class EntityWithNotMappedProperty
        {
            public int Id { get; set; }

            [NotMapped]
            public string Computed { get; set; } = string.Empty;
        }

        [Fact]
        public void NotMappedProperty_IsExcludedFromMappedProperties()
        {
            Assert.DoesNotContain(EntityInfoCache<EntityWithNotMappedProperty>.Properties, p => p.Name == "Computed");
        }

        private sealed class EntityWithColumnAttribute
        {
            public int Id { get; set; }

            [Column("full_name")]
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void ColumnAttribute_OverridesColumnName()
        {
            Assert.Equal("full_name", EntityInfoCache<EntityWithColumnAttribute>.ColumnNamesByPropertyName["Name"]);
        }

        private sealed class EntityWithDatabaseGeneratedIdentity
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void DatabaseGeneratedIdentityProperty_IsExcludedFromInsertAndUpdateProperties()
        {
            Assert.DoesNotContain(EntityInfoCache<EntityWithDatabaseGeneratedIdentity>.InsertProperties, p => p.Name == "Id");
            Assert.Contains(EntityInfoCache<EntityWithDatabaseGeneratedIdentity>.InsertProperties, p => p.Name == "Name");
            Assert.DoesNotContain(EntityInfoCache<EntityWithDatabaseGeneratedIdentity>.UpdateProperties, p => p.Name == "Id");
        }

        private sealed class EntityWithUpsertKey
        {
            public int Id { get; set; }

            [UpsertKey]
            public string Code { get; set; } = string.Empty;
        }

        [Fact]
        public void UpsertKeyAttribute_IsUsedInsteadOfIdentifier()
        {
            PropertyInfo item = Assert.Single(EntityInfoCache<EntityWithUpsertKey>.UpsertKeyProperties);
            Assert.Equal("Code", item.Name);
        }

        private sealed class EntityWithoutUpsertKey
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void NoUpsertKeyAttribute_DefaultsToIdentifierProperty()
        {
            PropertyInfo item = Assert.Single(EntityInfoCache<EntityWithoutUpsertKey>.UpsertKeyProperties);
            Assert.Equal("Id", item.Name);
        }
    }
}
