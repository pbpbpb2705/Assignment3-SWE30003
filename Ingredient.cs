using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Ingredient class representing an ingredient
    public class Ingredient
    {
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }

        public Ingredient(string name, decimal calories, decimal price, decimal stock)
        {
            Name = name;
            Calories = calories;
            Price = price;
            Stock = stock;
        }
    }
}
