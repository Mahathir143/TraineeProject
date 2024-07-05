namespace CountryCRUDApi.Common
{
    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object Data { get; set; }
    }
}
