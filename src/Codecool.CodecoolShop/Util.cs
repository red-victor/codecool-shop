using Codecool.CodecoolShop.Models;
using Newtonsoft.Json;
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

        public static List<CartItem> DeserializeJSON(string payload)
        {
            return JsonConvert.DeserializeObject<List<CartItem>>(payload);
        }

        public static string GetOrderLogDirectory()
        {
            return Environment.CurrentDirectory + "\\Logs\\Orders\\";
        }

        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
        }
    }
}
