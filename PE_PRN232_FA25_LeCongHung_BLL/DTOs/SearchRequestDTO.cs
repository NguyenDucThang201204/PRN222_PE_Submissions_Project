namespace PE_PRN232_FA25_LeCongHung_BLL.DTOs
{
    public class SearchRequestDTO
    {
        public int CurrentPage { get; set; } = 2;
        public int PageSize { get; set; } = 3;
        public string? BearName { get; set; }
        public decimal? BearWeight { get; set; }
        public string? BearTypeName { get; set; }
    }
}

