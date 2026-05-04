using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace Dapper.Forge.Core.Caching
{
    public static class EntityInfoCache<TEntity> where TEntity : class
    {
        public static string TableName { get; }
        public static PropertyInfo IdPropertyInfo { get; }
        public static ImmutableArray<PropertyInfo> PropertyInfos { get; }
        public static ImmutableArray<PropertyInfo> UpdatePropertyInfos { get; }
        public static ImmutableArray<PropertyInfo> InsertPropertyInfos { get;}
        public static ImmutableDictionary<string, string> ColumnNamesByPropertyName { get; }
        public static ImmutableDictionary<string, PropertyInfo> PropertyInfosByPropertyName { get; }
        public static ImmutableDictionary<string, Func<TEntity, object?>> PropertyGettersByPropertyName { get; }

        static EntityInfoCache()
        {
            Type entityType = typeof(TEntity);
            PropertyInfo? idNamedPropertyInfo = null;

            HashSet<string> seenPropertyNames = new(StringComparer.OrdinalIgnoreCase);
            Stack<PropertyInfo> stack = new();
            int keyCount = 0;

            for (Type? type = entityType; type is not null; type = type.BaseType)
            {
                foreach (PropertyInfo propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(x => !x.IsDefined(typeof(NotMappedAttribute), true))
                    .Reverse())
                {
                    if (!seenPropertyNames.Add(propertyInfo.Name))
                        continue;

                    if (propertyInfo.IsDefined(typeof(KeyAttribute), true))
                    {
                        if (keyCount++ > 1)
                            throw new InvalidOperationException($"Multiple properties in the entity {entityType} are marked with the [Key] attribute. Only one property can be marked as the key.");

                        IdPropertyInfo = propertyInfo;
                    }

                    if (propertyInfo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                        idNamedPropertyInfo = propertyInfo;

                    stack.Push(propertyInfo);
                }
            }

            if (IdPropertyInfo is null)
            {
                if (idNamedPropertyInfo is null)
                    throw new InvalidOperationException($"No property in the entity {entityType} is marked with the [Key] attribute or named 'Id'. One property must be marked as the key or named 'Id' to be used as the identifier for the entity.");

                IdPropertyInfo = idNamedPropertyInfo;
            }

            TableName = entityType.GetCustomAttribute<TableAttribute>()?.Name ?? $"{entityType.Name}s";

            PropertyInfos = [.. stack];
            UpdatePropertyInfos = [.. PropertyInfos.Where(x => !(x == IdPropertyInfo || x.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption > DatabaseGeneratedOption.None))];
            InsertPropertyInfos = [.. PropertyInfos.Where(x => !(x.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption > DatabaseGeneratedOption.None))];

            ImmutableDictionary<string, string>.Builder columnNamesBuilder = ImmutableDictionary.CreateBuilder<string, string>(StringComparer.OrdinalIgnoreCase);
            ImmutableDictionary<string, PropertyInfo>.Builder PropertyInfosBuilder = ImmutableDictionary.CreateBuilder<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            ImmutableDictionary<string, Func<TEntity, object?>>.Builder PropertyGettersBuilder = ImmutableDictionary.CreateBuilder<string, Func<TEntity, object?>>(StringComparer.OrdinalIgnoreCase);

            foreach (PropertyInfo propertyInfo in PropertyInfos)
            {
                columnNamesBuilder[propertyInfo.Name] = propertyInfo.GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyInfo.Name;
                PropertyInfosBuilder[propertyInfo.Name] = propertyInfo;

                MethodInfo? getMethod = propertyInfo.GetMethod;

                if (getMethod is not null)
                    PropertyGettersBuilder[propertyInfo.Name] = PropertyHelper.BuildGetterExpression<TEntity>(propertyInfo);
            }

            ColumnNamesByPropertyName = columnNamesBuilder.ToImmutable();
            PropertyInfosByPropertyName = PropertyInfosBuilder.ToImmutable();
            PropertyGettersByPropertyName = PropertyGettersBuilder.ToImmutable();
        }
    }
}
