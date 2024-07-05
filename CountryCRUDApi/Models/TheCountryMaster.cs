using System;
using System.Collections.Generic;

namespace CountryCRUDApi.Models
{
    public partial class TheCountryMaster
    {
        public int Id { get; set; }
        public string? CountryCode { get; set; }
        public string? Name { get; set; }
    }
}
