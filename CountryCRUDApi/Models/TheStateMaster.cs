using System;
using System.Collections.Generic;

namespace CountryCRUDApi.Models
{
    public partial class TheStateMaster
    {
        public int Id { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public int? CountryId { get; set; }
    }
}
