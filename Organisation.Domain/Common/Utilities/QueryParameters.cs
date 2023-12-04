
namespace Organisation.Domain.Common.Utilities;

public class QueryParameters
{
    private int _maxPageSize = 100;
    private int _pageSize = 100;
    private string _sortOrder = "asc";

    public int PageNo { get; set; } = 1;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = Math.Min(_maxPageSize, value);
        }
    }
    public string SortBy { get; set; } = "PagingOrder";
    public string SortOrder
    {
        get
        {
            return _sortOrder;
        }
        set
        {
            if (value == "asc" || value == "desc")
                _sortOrder = value;
        }
    }
}
