using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Set: PayableComponent
    {
        List<Dish> set;
        public Set(string name, decimal price) : base(name, price)
        { 
        }
    }
}
