namespace CountryCRUDApi.Common
{
    public class StateRequest
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode  { get; set; }
        public object?[]? Id { get; internal set; }
    }
}
