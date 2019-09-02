using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzTGWebAppEntities
{
    public class UpstoxLoginLogoutStatus
    {
        public string LoginResponse { get; set; }
        public bool LoginStatus { get; set; }
        public string LoginURL { get; set; }
        public bool LogoutStatus { get; set; }

    }
}