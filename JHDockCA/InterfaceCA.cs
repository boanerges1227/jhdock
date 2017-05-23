using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHDockCA
{
    public interface InterfaceCA
    {
         bool ReadKey();
         bool UserLogin(int certID, string PinMa);

    }
}
