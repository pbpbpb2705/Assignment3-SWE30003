﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Menu class to display and manage menu items
    public class Menu
    {
        private Database _db;
        private int _nextID = 1; // Counter for IDs

        public Menu(Database db)
        {
            _db = db;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("----- Menu -----");
            var dishes = _db.Dishes.FindAll();
            var sets = _db.Sets.FindAll();

            // Display dishes
            Console.WriteLine("Dishes:");
            foreach (var dish in dishes)
            {
                Console.WriteLine($"{dish.ID} - {dish.Name} - ${dish.Price}"); // Use dish.ID directly
            }

            // Display sets
            Console.WriteLine("Sets:");
            foreach (var set in sets)
            {
                Console.WriteLine($"{set.ID} - {set.Name} - ${set.Price}"); // Use set.ID directly
            }

            Console.WriteLine("------------------");
        }

        // Function to add a dish to the menu
        public void AddDish(string name, decimal price)
        {
            var newDish = new Dish(name, price) { ID = _nextID };
            _db.Dishes.Insert(newDish);
            _nextID++; // Increment dish ID counter
            Console.WriteLine($"Dish '{name}' added to the menu.");
        }

        // Function to add a set to the menu
        public void AddSet(string name, decimal price, List<Dish> dishes)
        {
            var newSet = new Set(name, price) { ID = _nextID, Dishes = dishes };
            _db.Sets.Insert(newSet);
            _nextID++; // Increment set ID counter
            Console.WriteLine($"Set '{name}' added to the menu.");
        }

        public PayableComponent ChooseItem(int choice)
        {
            // Check if the choice is for a Dish or a Set
            if (choice > 0)
            {
                // Find Dish by ID
                var dish = _db.Dishes.FindOne(x => x.ID == choice);
                if (dish != null)
                {
                    return dish;
                }

                // Find Set by ID
                var set = _db.Sets.FindOne(x => x.ID == choice);
                if (set != null)
                {
                    return set;
                }
            }

            Console.WriteLine("Invalid choice. Please try again.");
            return null;
        }
    }
}