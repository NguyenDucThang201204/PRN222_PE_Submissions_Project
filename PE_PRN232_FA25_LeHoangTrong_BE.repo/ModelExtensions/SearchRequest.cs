namespace PE_PRN232_FA25_LeHoangTrong_BE.repo.ModelExtensions;

public class SearchRequest
{
    public SearchRequest(int? currentPage, int? pageSize)
    {
        if (currentPage.HasValue && currentPage > 0) CurrentPage = currentPage;
        if (pageSize.HasValue && pageSize > 0) PageSize = pageSize;
    }

    public int? CurrentPage { get; set; } = 2;
    public int? PageSize { get; set; } = 3;
}

public sealed class BearSearchRequest : SearchRequest
{

    public BearSearchRequest(string? bearName, decimal? bearWeight, string? bearTypeName, int? currentPage, int? pageSize) : base(currentPage, pageSize)
    {
        BearName = bearName;
        BearWeight = bearWeight;
        BearTypeName = bearTypeName;
    }

    public string? BearName { get; set; }
    public decimal? BearWeight { get; set; }
    public string? BearTypeName { get; set; }
}