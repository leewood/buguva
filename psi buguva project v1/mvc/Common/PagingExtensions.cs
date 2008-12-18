using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq.Dynamic;

namespace mvc.Common
{
	public static class PagingExtensions
	{
		#region HtmlHelper extensions

		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null);
		}

		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, null);
		}

		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values));
		}

		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, object values)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values));
		}

		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, valuesDictionary);
		}

		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary)
		{
			if (valuesDictionary == null)
			{
				valuesDictionary = new RouteValueDictionary();
			}
			if (actionName != null)
			{
				if (valuesDictionary.ContainsKey("action"))
				{
					throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
				}
				valuesDictionary.Add("action", actionName);
			}
			var pager = new Pager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary);
			return pager.RenderHtml();
		}

		#endregion

		#region IQueryable<T> extensions

		public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
		{
			return new PagedList<T>(source, pageIndex, pageSize);
		}

        public static List<T> Filter<T>(this IQueryable<T> source, string filterClause)
		{
            object[] parameters = new object[0];
            return source.Where(filterClause, parameters).ToList();
           

            //DynamicLinq.ParseToFunction<T, Boolean>(filterClause);
		}

        public static List<T> Filter<T>(this IEnumerable<T> source, string filterClause)
        {
            object[] parameters = new object[0];
            if ((filterClause == null) || (filterClause == ""))
            {
                return source.ToList();
            }
            try
            {
                return source.AsQueryable().Where(filterClause, parameters).ToList();
            }
            catch (Exception)
            {

                string newFilter = "";
                string next = "";
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (System.Reflection.PropertyInfo info in properties)
                {
                    newFilter += next + "ExtensionMethods.Like(" + info.Name + ", \"" + filterClause + "\") ";
                    next = " || ";
                }
                try
                {
                    return source.AsQueryable().Where(newFilter, parameters).ToList();
                }
                catch (Exception)
                {
                    return new List<T>();
                }
            }
            


            //DynamicLinq.ParseToFunction<T, Boolean>(filterClause);
        }

        public static List<T> Sort<T>(this IEnumerable<T> source, string sortClause)
        {   
            object[] parameters = new object[0];
            return source.AsQueryable().OrderBy(sortClause, parameters).ToList();
        }

		public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedList<T>(source, pageIndex, pageSize, totalCount);
		}

		#endregion

		#region IEnumerable<T> extensions

		public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
		{
			return new PagedList<T>(source, pageIndex, pageSize);
		}

		public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedList<T>(source, pageIndex, pageSize, totalCount);
		}

		#endregion
	}
}