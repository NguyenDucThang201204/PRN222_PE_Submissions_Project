namespace Repositories.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

    }

    public class SearchRequestDto : SearchRequest
    {
        public string? BearName { get; set; }

        public string? TypeName { get; set; }

        //public decimal? Amount { get; set; }
    }
}
