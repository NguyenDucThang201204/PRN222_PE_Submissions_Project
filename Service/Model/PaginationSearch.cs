namespace Service.Model
{
    public class PaginationSearch
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string BearName { get; set; } = string.Empty;
        public double BearWeight { get; set; }
        public int BearTypeName { get; set; }
    }
}
