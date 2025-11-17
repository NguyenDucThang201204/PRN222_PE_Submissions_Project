namespace PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions
{
    public record PagingResponse<T>(IEnumerable<T>? Items, int TotalCount, int PageNumber, int PageSize) where T : class;  
}
