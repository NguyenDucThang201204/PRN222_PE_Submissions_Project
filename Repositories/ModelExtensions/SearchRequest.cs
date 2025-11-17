namespace Repositories.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; } = 2;

        public int? PageSize { get; set; } = 3;
    }

    public class MainSearchRequest : SearchRequest
    {
        public string? BearName { get; set; }

        public double? BearWeight { get; set; }

        public string? BearTypeName { get; set; }
    }
}
