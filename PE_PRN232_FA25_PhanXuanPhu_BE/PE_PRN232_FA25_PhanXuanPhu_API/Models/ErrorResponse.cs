namespace PE_PRN232_FA25_PhanXuanPhu_API.Models
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public ErrorResponse(string code, string message)
        {
            ErrorCode = code;
            Message = message;
        }
    }
}
