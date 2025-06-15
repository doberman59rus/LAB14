using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchLibrary
{
    public class WatchBrandComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                throw new ArgumentException("Объекты не могут быть null");

            Watch watch1 = x as Watch;
            Watch watch2 = y as Watch;

            if (watch1 == null || watch2 == null)
                throw new ArgumentException("Объекты не являются типом Часы");

            return string.Compare(watch1.Brand, watch2.Brand);
        }
    }
}
