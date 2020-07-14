using System;
using System.Collections.Generic;

namespace ReportWebApp.TOTVASModels
{
    public partial class UserViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGroup { get; set; }
        public long? UserStatus { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
