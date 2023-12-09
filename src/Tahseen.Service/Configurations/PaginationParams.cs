namespace Tahseen.Service.Configurations;

public class PaginationParams
{
    private const int _maxPageSize = 20;
    private int _pageSize;
    public int PageSize
    {
        set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
        get => _pageSize == 0 ? 10 : _pageSize;
    }
    public int PageIndex { get; set; } = 1;
    
}
