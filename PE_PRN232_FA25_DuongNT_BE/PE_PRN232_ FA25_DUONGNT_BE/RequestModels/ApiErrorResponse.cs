namespace PE_PRN232__FA25_DUONGNT_BE.RequestModels
{
    public class ApiErrorResponse
    {
        public string ErrorCode { get; set; } = null!;
        public int Status { get; set; }
        public string Message { get; set; } = null!;
    }
}
