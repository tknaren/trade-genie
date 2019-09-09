using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public interface IUpstoxInterface
    {
        bool InitializeUpstox(string apiKey, string apiSecret, string redirectUrl);
        bool SetUpstoxAccessToken(string accesToken);
    }
}
