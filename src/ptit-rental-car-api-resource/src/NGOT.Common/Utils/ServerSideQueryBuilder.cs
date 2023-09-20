using NGOT.Common.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Ardalis.GuardClauses;

namespace NGOT.Common.Utils;

public class ServerSideQueryBuilder<TEntity> where TEntity : Entity
{
    private IQueryable<TEntity> _queryable;
    private readonly ServerSideQueryRequest _request;
    private const string _ascending = "asc";
    private const string _descending = "desc";
    
    public ServerSideQueryBuilder(IQueryable<TEntity> queryable, ServerSideQueryRequest request)
    {
        _queryable = Guard.Against.Null(queryable, nameof(queryable));
        _request = Guard.Against.Null(request, nameof(request));
    }

    public ServerSideQueryBuilder<TEntity> ApplySearch()
    {
        StringBuilder sbFilterExpression = new();
        Type type = typeof(TEntity);
        foreach (var searchOption in _request.Search)
        {
            sbFilterExpression.Clear();
            
            if (!searchOption.Fields.Any() || string.IsNullOrEmpty(searchOption.Key)) 
                continue;

            foreach (var field in searchOption.Fields)
            {
                if (type.GetMethod(searchOption.Operator!, BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    sbFilterExpression.Append($"{field}.{searchOption.Operator}(@0) || ");
                }
                else
                {
                    sbFilterExpression.Append($"{field}.Equals(@0) || ");
                }
            }
            
            string filterExpression = sbFilterExpression.ToString().TrimEnd(' ', '|');
            
            _queryable = _queryable.Where(filterExpression, searchOption.Key);
        }

        return this;
    }
    
    public ServerSideQueryBuilder<TEntity> ApplyFilter()
    {
        if (!_request.Where.Any())
            return this;

        StringBuilder sbFilterExpression = new();
        Type type = typeof(TEntity);
        foreach (var whereOption in _request.Where)
        {
            sbFilterExpression.Clear();
            
            foreach (var field in whereOption.Predicates)
            {
                if (type.GetMethod(whereOption.Operator!, BindingFlags.Public | BindingFlags.Instance) != null)
                {
                    sbFilterExpression.Append($"{field.Field}.{whereOption.Operator}(@0) || ");
                }
                else
                {
                    sbFilterExpression.Append($"{field}.Equals(@0) || ");
                }
            }
            
            string filterExpression = sbFilterExpression.ToString().TrimEnd(' ', '|');
            
            _queryable = _queryable.Where(filterExpression, whereOption.Value);
        }

        return this;
    }
    
    public ServerSideQueryBuilder<TEntity> ApplySort()
    {
        if (!_request.Sort.Any())
            return this;

        string sortExpression = string.Empty;
        var sortAscending = _request
            .Sort
            .Where(x => x.Direction == ServerSideQueryRequest.DirectionOpt.Ascending)
            .Select(x => x.Name)
            .ToList();

        if (sortAscending.Any())
        {
            sortExpression = string.Join(",", sortAscending) + " " + _ascending;
        }
        
        var sortDescending = _request
            .Sort
            .Where(x => x.Direction == ServerSideQueryRequest.DirectionOpt.Descending)
            .Select(x => x.Name)
            .ToList();
        
        if (sortDescending.Any())
        {
            if (!string.IsNullOrEmpty(sortExpression))
            {
                sortExpression += ",";
            }
            
            sortExpression += string.Join(",", sortDescending) + " " + _descending;
        }

        if (!string.IsNullOrEmpty(sortExpression))
        {
            _queryable = _queryable.OrderBy(sortExpression);
        }
        
        return this;
    }
    
    public ServerSideQueryBuilder<TEntity> ApplyPaging()
    {
        if (_request.Take.HasValue)
        {
            _queryable = _queryable.Take(_request.Take.Value);
        }
        if (_request.Skip.HasValue)
        {
            _queryable = _queryable.Skip(_request.Skip.Value);
        }

        return this;
    }
    
    public ServerSideQueryBuilder<TEntity> ApplyGroup()
    {
        if (!_request.Group.Any())
            return this;

        // TODO: Implement grouping
        return this;
    }
    
    public IQueryable<TEntity> Build()
    {
        return _queryable;
    }
}