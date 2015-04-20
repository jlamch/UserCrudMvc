using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Database.EF.Helpers
{
    /// <summary>
    /// Helper class for EntityFramework.
    /// </summary>
    public static class EntityFrameworkHelper
    {
        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <typeparam name="TValue">Type.</typeparam>
        /// <returns>Property name.</returns>
        public static string GetPropertyName<TValue>(Expression<Func<TValue, object>> selector)
        {
            PropertyInfo prop = null;
            prop = GetProperty<TValue>(selector);
            return prop != null
                   ? prop.Name
                   : string.Empty;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <typeparam name="TValue">Type.</typeparam>
        /// <returns>Property info.</returns>
        public static PropertyInfo GetProperty<TValue>(Expression<Func<TValue, object>> selector)
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }

            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)body).Member as PropertyInfo;
                case ExpressionType.Convert:
                    UnaryExpression unaryExpression = body as UnaryExpression;
                    if (unaryExpression != null)
                    {
                        return (unaryExpression.Operand as MemberExpression).Member as PropertyInfo;
                    }

                    throw new InvalidOperationException();
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the name of the table.
        /// Will work witn EF Version 6.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="workspace">The workspace.</param>
        /// <returns>
        /// Table name do T.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Unable to find entity set corresponding to given entity type in edm metadata.</exception>
        public static string GetTableName<T>(System.Data.Entity.Core.Metadata.Edm.MetadataWorkspace workspace)
        {
            var entitySet = GetEntitySet<T>(workspace);
            if (entitySet == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find entity set '{0}' in edm metadata", typeof(T).Name));
            }

            var tableName = string.Format("[{0}].[{1}]", GetStringProperty(entitySet, "Schema"), GetStringProperty(entitySet, "Table"));
            return tableName;
        }

        /// <summary>
        /// Gets the entity set.
        /// Will work witn EF Version 6.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="workspace">The workspace.</param>
        /// <returns>
        /// Entity set for T.
        /// </returns>
        private static EntitySet GetEntitySet<T>(MetadataWorkspace workspace)
        {
            var type = typeof(T);
            var entityName = type.Name;
            var metadata = workspace;

            IEnumerable<System.Data.Entity.Core.Metadata.Edm.EntitySet> entitySets;
            entitySets = metadata.GetItemCollection(System.Data.Entity.Core.Metadata.Edm.DataSpace.SSpace)
                                 .GetItems<System.Data.Entity.Core.Metadata.Edm.EntityContainer>()
                                 .Single()
                                 .BaseEntitySets
                                 .OfType<System.Data.Entity.Core.Metadata.Edm.EntitySet>()
                                 .Where(s => !s.MetadataProperties.Contains("Type") ||
                                     s.MetadataProperties["Type"].ToString() == "Tables");
            var entitySet = entitySets.FirstOrDefault(t => t.Name == entityName);
            return entitySet;
        }

        /// <summary>
        /// Gets the string property.
        /// Will work witn EF Version 6.
        /// </summary>
        /// <param name="entitySet">The entity set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Table name for entity set.</returns>
        /// <exception cref="System.ArgumentNullException">Value of entitySet is null.</exception>
        private static string GetStringProperty(System.Data.Entity.Core.Metadata.Edm.MetadataItem entitySet, string propertyName)
        {
            System.Data.Entity.Core.Metadata.Edm.MetadataProperty property;
            if (entitySet == null)
            {
                throw new ArgumentNullException("entitySet");
            }

            if (entitySet.MetadataProperties.TryGetValue(propertyName, false, out property))
            {
                string str = null;
                if (((property != null) &&
                    (property.Value != null)) &&
                    (((str = property.Value as string) != null) &&
                        !string.IsNullOrWhiteSpace(str)))
                {
                    return str;
                }
            }

            return string.Empty;
        }
    }
}