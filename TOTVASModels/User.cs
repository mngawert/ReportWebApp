using System;
using System.Collections.Generic;

namespace ReportWebApp.TOTVASModels
{
    public partial class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
    }
}
