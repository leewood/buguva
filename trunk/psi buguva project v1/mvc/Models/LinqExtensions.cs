﻿using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToSqlExtensions
{
    public static class DataContextExtensions
    {

        public static IQueryable<TEntity> Select<TEntity>(this DataContext dataContext) where TEntity : class
        {
            return dataContext.GetTable<TEntity>();
        }

        public static TEntity Get<TEntity>(this DataContext dataContext, object id) where TEntity : class, new()
        {
            return Get<TEntity>(dataContext, id, "id");
        }

        public static TEntity Get<TEntity>(this DataContext dataContext, object id, string primaryKeyName) where TEntity : class, new()
        {
            if (id == null)
                return new TEntity();

            var table = dataContext.GetTable<TEntity>();
            return table.Single(DynamicGet<TEntity>(primaryKeyName, id));
        }

        public static TEntity Save<TEntity>(this DataContext dataContext, NameValueCollection formParams, int? id) where TEntity : class, new()
        {
            return Save<TEntity>(dataContext, formParams, id, "id");
        }


        public static TEntity Save<TEntity>(this DataContext dataContext, NameValueCollection formParams, int? id, string primaryKeyName) where TEntity : class, new()
        {

            if (id == null || id < 1)
                return Insert<TEntity>(dataContext, formParams);
            else
                return Update<TEntity>(dataContext, formParams, id.Value);
        }


        public static TEntity Insert<TEntity>(this DataContext dataContext, NameValueCollection formParams) where TEntity : class, new()
        {
            // Create entity
            var entityToInsert = CreateEntityFromForm<TEntity>(dataContext, formParams);

            // Submit new entity
            var table = dataContext.GetTable<TEntity>();
            table.InsertOnSubmit(entityToInsert);
            dataContext.SubmitChanges();

            // Return entity
            return entityToInsert;
        }

        public static TEntity Update<TEntity>(this DataContext dataContext, NameValueCollection formParams, object id) where TEntity : class, new()
        {
            return Update<TEntity>(dataContext, formParams, id, "id");
        }

        public static TEntity Update<TEntity>(this DataContext dataContext, NameValueCollection formParams, object id, string primaryKeyName) where TEntity : class, new()
        {
            // Get matching entity
            var entityToUpdate = dataContext.Get<TEntity>(id, primaryKeyName);

            // Update entity
            UpdateEntityFromForm(dataContext, entityToUpdate, formParams);

            // Save changes
            dataContext.SubmitChanges();
            return entityToUpdate;
        }

        public static void Delete<TEntity>(this DataContext dataContext, object id) where TEntity : class, new()
        {
            Delete<TEntity>(dataContext, id, "id");
        }


        public static void Delete<TEntity>(this DataContext dataContext, object id, string primaryKeyName) where TEntity : class, new()
        {
            // Get matching entity
            var entityToDelete = Get<TEntity>(dataContext, id, primaryKeyName);

            // Delete it
            var table = dataContext.GetTable<TEntity>();
            table.DeleteOnSubmit(entityToDelete);
            dataContext.SubmitChanges();
        }

        public static TEntity CreateEntityFromForm<TEntity>(this DataContext dataContext, NameValueCollection formParams) where TEntity : class, new()
        {
            var entity = new TEntity();
            foreach (var propName in formParams.AllKeys)
            {
                var prop = GetCaseInsensitivePropertyInfo<TEntity>(propName);
                if (prop != null)
                {
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    var propValue = converter.ConvertFromInvariantString(formParams[prop.Name]);
                    prop.SetValue(entity, propValue, null);
                }
            }
            return entity;
        }


        public static TEntity UpdateEntityFromForm<TEntity>(this DataContext dataContext, TEntity entityToUpdate, NameValueCollection formParams) where TEntity : class, new()
        {
            foreach (var propName in formParams.AllKeys)
            {
                var prop = GetCaseInsensitivePropertyInfo<TEntity>(propName);
                if (prop != null)
                {
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    var propValue = converter.ConvertFromInvariantString(formParams[prop.Name]);
                    prop.SetValue(entityToUpdate, propValue, null);
                }
            }
            return entityToUpdate;
        }



        private static Expression<Func<TEntity, bool>> DynamicGet<TEntity>(string primaryKeyName, object id)
        {
            var e = Expression.Parameter(typeof(TEntity), "e");
            var propInfo = GetCaseInsensitivePropertyInfo<TEntity>(primaryKeyName);
            var m = Expression.MakeMemberAccess(e, propInfo);
            var c = Expression.Constant(id);
            var b = Expression.Equal(m, c);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(b, e);
            return lambda;
        }

        private static PropertyInfo GetCaseInsensitivePropertyInfo<TEntity>(string propertyName)
        {
            return typeof(TEntity).GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        }


    }
}