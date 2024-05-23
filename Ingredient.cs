using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Ingredient
    {
        private Database _db;
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public Ingredient(string name, decimal calories) 
        { 
            Name = name;
            Calories = calories;
        }
        public bool CheckAvail(Ingredient ingredient) 
        {
            return _db.CheckAvail(ingredient);
        }

        public decimal CheckStock(Ingredient ingredient)
        {
            return _db.CheckStock(ingredient);
        }

        public void UpdatePrice()
        {
         
        }
    }
}
