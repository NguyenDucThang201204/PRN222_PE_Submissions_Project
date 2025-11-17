namespace PE_PRN232_FA25_LeCongHung_BLL.DTOs
{
    public class SearchResponseDTO<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}

