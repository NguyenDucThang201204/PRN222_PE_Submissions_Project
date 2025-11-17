namespace Service.Model
{
    public class PaginationResponse
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<object> Items { get; set; } = Enumerable.Empty<object>();
    }
}
