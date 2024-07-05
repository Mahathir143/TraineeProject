using System;
using System.Collections.Generic;

namespace CountryCRUDApi.Models
{
    public partial class TblRoleMaster
    {
        public int Id { get; set; }
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public bool? Isactive { get; set; }
    }
}
