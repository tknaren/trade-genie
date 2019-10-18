using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIInterfaceLayer.Models
{
    public class Historical
    {
        public int code { get; set; }
        public string status { get; set; }
        public DateTime timestamp { get; set; }
        public string message { get; set; }
        public List<string> data { get; set; }
        public Error error { get; set; }
    }
}
