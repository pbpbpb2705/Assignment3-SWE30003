using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Dish class representing a dish
    public class Dish : PayableComponent
    {
        public List<Ingredient> Ingredients = new List<Ingredient>();
        public Dish(string name, decimal price) : base(name, price)
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
