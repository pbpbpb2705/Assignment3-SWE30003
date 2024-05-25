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
        public List<Ingredient> Ingredients { get; set; }
        public Dish(string name, decimal price, List<Ingredient> ingredient) : base(name, price)
        {
            Ingredients = new List<Ingredient>();
            Name = name;
            Price = price;
        }
    }
}
