namespace Organisation.Application.Common.Utilities;

public class PageList<T>
{
    private PageList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public IEnumerable<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    public bool HasPreviousPage => PageNumber > 1;

    public static PageList<T> Create(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
    {
        return new PageList<T>(items, pageNumber, pageSize, totalCount);
    }

}
