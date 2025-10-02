using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UPXV.Common.Page;
using UPXV.Models;

namespace UPXV.Data;

public static class QueryExtensions
{
   public static IQueryable<T> ApplyQuery<T> (this IQueryable<T> queryable, Query<T> query) 
      where T : class => query.ApplyTo(queryable);
}
public record Query<TEntity> where TEntity : class
{
   private int _skip;
   private int _take;
   private bool _asNoTracking;
   private readonly ICollection<Expression<Func<TEntity, bool>>> _filters = [];
   private readonly ICollection<Expression<Func<TEntity, object>>> _includes = [];
   private readonly ICollection<(Expression<Func<TEntity, object>> Expression, bool Descending)> _sortings = [];
   public Query() { }
   public Query (PageDTO<TEntity> page)
   {
      if (page is null) throw new ArgumentNullException(nameof (page));
      _skip = page.CurrentPage * page.PageSize;
      _take = page.PageSize;
   }
   public Query<TEntity> Skip (int skip)
   {
      _skip = skip;
      return this;
   }
   public Query<TEntity> Take (int take)
   {
      _take = take;
      return this;
   }
   public Query<TEntity> Paging(int index, int size)
   {
      _skip = index * size;
      _take = size;
      return this;
   }
   public Query<TEntity> AsNoTracking ()
   {
      _asNoTracking = true;
      return this;
   }
   public Query<TEntity> Filter (Expression<Func<TEntity, bool>> predicate)
   {
      _filters.Add(predicate);
      return this;
   }
   public Query<TEntity> Include (Expression<Func<TEntity, object>> property)
   {
      _includes.Add(property);
      return this;
   }
   public Query<TEntity> SortBy<TKey> (Expression<Func<TEntity, TKey>> expression)
   {
      _sortings.Add((expression as Expression<Func<TEntity, object>>, false)!);
      return this;
   }
   
   public Query<TEntity> SortByDescending<TKey> (Expression<Func<TEntity, TKey>> expression)
   {
      _sortings.Add((expression as Expression<Func<TEntity, object>>, true)!);
      return this;
   }

   public IQueryable<TEntity> ApplyTo (IQueryable<TEntity> queryable)
   {
      if(queryable is null)
      {
         throw new ArgumentNullException(nameof(queryable));
      }

      if (_asNoTracking)
      {
         queryable = queryable.AsNoTracking();
      }

      foreach (var include in _includes)
      {
         queryable = queryable.Include(include);
      }

      foreach (var filter in _filters)
      {
         queryable = queryable.Where(filter);
      }

      if (_sortings.Any())
      {
         var firstSorting = _sortings.First();
         var sortedQueryable = firstSorting.Descending
            ? queryable.OrderByDescending(firstSorting.Expression)
            : queryable.OrderBy(firstSorting.Expression);
            
         foreach (var sorting in _sortings.Skip(1))
         {
            sortedQueryable = sorting.Descending
               ? sortedQueryable.ThenByDescending(sorting.Expression)
               : sortedQueryable.ThenBy(sorting.Expression);
         }

         queryable = sortedQueryable;
      }

      if (_skip > 0)
      {
         queryable = queryable.Skip(_skip);
      }

      if (_take > 0)
      {
         queryable = queryable.Take(_take);
      }

      return queryable;
   }
}