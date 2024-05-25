using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Assignment3
{
    public class Database
    {
        private LiteDatabase _db;

        public Database(LiteDatabase db)
        {
            _db = db;
        }

        // Methods to interact with database collections
        public ILiteCollection<Dish> Dishes => _db.GetCollection<Dish>("Dishes");
        public ILiteCollection<Ingredient> Ingredients => _db.GetCollection<Ingredient>("Ingredients");
        public ILiteCollection<Customer> Customers => _db.GetCollection<Customer>("Customers");
        public ILiteCollection<Order> Orders => _db.GetCollection<Order>("Orders");
        public ILiteCollection<Set> Sets => _db.GetCollection<Set>("Sets");
        public ILiteCollection<Reservation> Reservations => _db.GetCollection<Reservation>("Reservations");

        // Check if an ingredient is available
        public bool CheckAvail(Ingredient ingredient)
        {
            return Ingredients.FindOne(x => x.Name == ingredient.Name).Stock > 0;
        }

        // Check the stock of an ingredient
        public decimal CheckStock(Ingredient ingredient)
        {
            return Ingredients.FindOne(x => x.Name == ingredient.Name).Stock;
        }

        // Update the price of an ingredient
        public void UpdatePrice(Ingredient ingredient)
        {
            var existingIngredient = Ingredients.FindOne(x => x.Name == ingredient.Name);
            if (existingIngredient != null)
            {
                existingIngredient.Price = ingredient.Price;
                Ingredients.Update(existingIngredient);
            }
        }
    }
}