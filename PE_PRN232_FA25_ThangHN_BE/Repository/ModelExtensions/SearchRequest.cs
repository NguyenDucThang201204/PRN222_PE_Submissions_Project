namespace Repository.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

    }

    public class SearchRequestDto : SearchRequest
    {
        public string? BearName { get; set; }
        public double? BearWeight { get; set; }
        public string? BearTypeName { get; set; }
    }
}
