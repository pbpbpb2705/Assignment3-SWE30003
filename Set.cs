using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Set class representing a set menu
    public class Set : PayableComponent
    {
        public List<Dish> Dishes { get; set; }

        public Set(string name, decimal price) : base(name, price)
        {
            Dishes = new List<Dish>();
        }
    }
}
