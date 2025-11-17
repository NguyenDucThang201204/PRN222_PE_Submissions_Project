namespace FA25Bear.DataAccess.Models.DTOs
{
    public class ObjectResponse<T>
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ObjectResponse(string errorCode, string message, T? data)
        {
            ErrorCode = "200";
            Message = "success";
            Data = data;
        }
    }
}
