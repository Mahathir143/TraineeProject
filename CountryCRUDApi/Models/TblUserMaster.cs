using System;
using System.Collections.Generic;

namespace CountryCRUDApi.Models
{
    public partial class TblUserMaster
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? RoleId { get; set; }
    }
}
