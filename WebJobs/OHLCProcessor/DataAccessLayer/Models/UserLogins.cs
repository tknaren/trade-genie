using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class UserLogins
    {
        public int Id { get; set; }
        public string Broker { get; set; }
        public string ClientId { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
        public string RequestToken { get; set; }
        public string AccessToken { get; set; }
        public string PublicToken { get; set; }
        public string Status { get; set; }
    }
}
