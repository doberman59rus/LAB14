using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB14
{
    public class Shop
    {
        public string Name { get; set; }       // Название магазина
        public string Address { get; set; }    // Адрес магазина
        public List<string> Brands { get; set; } // Бренды часов в магазине
    }
}
