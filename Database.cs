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
        private static Database? _db;
        private LiteDatabase _litedb;

        private Database(LiteDatabase db)
        {
            _litedb = db;
        }

        public static Database getDatabase()
        {
            if (_db == null)
            {
                _db = new Database(new LiteDatabase("restaurant.db"));
            }
            return _db;
        }

        // Methods to interact with database collections
        public ILiteCollection<Dish> Dishes => _litedb.GetCollection<Dish>("Dishes");
        public ILiteCollection<Ingredient> Ingredients => _litedb.GetCollection<Ingredient>("Ingredients");
        public ILiteCollection<Customer> Customers => _litedb.GetCollection<Customer>("Customers");
        public ILiteCollection<Order> Orders => _litedb.GetCollection<Order>("Orders");
        public ILiteCollection<Set> Sets => _litedb.GetCollection<Set>("Sets");
        public ILiteCollection<Reservation> Reservations => _litedb.GetCollection<Reservation>("Reservations");

        // Check the stock of an ingredient
        public decimal CheckStock(Ingredient ingredient)
        {
            foreach (Ingredient ing in Ingredients.FindAll())
            {
                if (ing.Name == ingredient.Name)
                {
                    return ing.Stock;
                }
            }
            return -1;
        }

        // Update the price of an ingredient
        public void Update(Ingredient ingredient)
        {
            foreach (Ingredient ing in Ingredients.FindAll())
            {
                if (ing.Name == ingredient.Name)
                {
                    Ingredients.Update(ingredient);
                }
            }
        }
    }
}