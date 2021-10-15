using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop
{
    public static class Util
    {
        public static string GenerateID()
        {
            Guid myuuid = Guid.NewGuid();
            return myuuid.ToString();
        }
    }
}
