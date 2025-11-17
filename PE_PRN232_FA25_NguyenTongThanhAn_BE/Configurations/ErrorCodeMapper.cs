namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Configurations
{
    public static class ErrorCodeMapper
    {
        public static readonly Dictionary<int, (string Code, string Message)> Map = new()
    {
        { 400, ("400", "Missing/invalid input") },
        { 401, ("401", "Token missing/invalid") },
        { 403, ("403", "Permission denied") },
        { 404, ("404", "Resource not found") },
        { 500, ("500", "Internal server error") }
    };

        public static (string Code, string Message) Get(int statusCode)
        {
            return Map.TryGetValue(statusCode, out var result)
                ? result
                : ("500", "Internal server error");
        }
    }

}
