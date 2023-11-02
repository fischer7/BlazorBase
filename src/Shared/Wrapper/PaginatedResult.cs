using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalShared.Wrapper;

public sealed class PaginatedQueryResult<T>
{
    public PaginatedQueryResult() { }
    public IReadOnlyCollection<T>? Items { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedQueryResult(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public static PaginatedQueryResult<T> CreateFromQueryable(IQueryable<T> source, int pageNumber, int pageSize)
    {
        if (source is null)
            return new PaginatedQueryResult<T>(Enumerable.Empty<T>().ToList(), 0, pageNumber, pageSize);

        var items = source.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        int count = source.Count();

        return new PaginatedQueryResult<T>(items, count, pageNumber, pageSize);
    }
    public static PaginatedQueryResult<T> CreateMaterializedPaginated(IEnumerable<T> source, int pageNumber, int pageSize, int? customTotalCount = null)
    {
        if (source is null)
            return new PaginatedQueryResult<T>(Enumerable.Empty<T>().ToList(), 0, pageNumber, pageSize);

        var items = source is List<T> ? source as List<T> : source.ToList();
        int count = customTotalCount ?? items?.Count() ?? 0;

        return new PaginatedQueryResult<T>(items ?? Enumerable.Empty<T>().ToList(), count, pageNumber, pageSize);
    }
}
