using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpstoxNet;

namespace APIInterfaceLayer
{
    public interface IUpstoxInterface
    { }

    public class UpstoxInterface : IUpstoxInterface
    {
        public UpstoxInterface()
        {
            Upstox upstox = new Upstox();

        }
    }
}
