using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Menu class to display and manage menu items
    public class Menu
    {
        private int _nextID = 1; // Counter for IDs

        public Menu()
        {
           
        }

        public void DisplayMenu()
        {
            Console.WriteLine("----- Menu -----");
            var dishes = Database.getDatabase().Dishes.FindAll();
            var sets = Database.getDatabase().Sets.FindAll();

            // Display dishes
            Console.WriteLine("Dishes:");
            foreach (var dish in dishes)
            {
                Console.WriteLine($"{dish.ID} - {dish.Name} - ${dish.Price}");

                // Display ingredients for the dish
                Console.WriteLine("   Ingredients:");
                if (dish.Ingredients != null && dish.Ingredients.Count > 0) // Check for null or empty list
                {
                    foreach (var ingredient in dish.Ingredients)
                    {
                        Console.WriteLine($"     - {ingredient.Name} (Calories: {ingredient.Calories}, Price: ${ingredient.Price}, Stock: {ingredient.Stock})");
                    }
                }
                else
                {
                    Console.WriteLine("     - No ingredients listed.");
                }
            }

            // Display sets (You might want to display ingredients for sets as well)
            Console.WriteLine("\nSets:");
            foreach (var set in sets)
            {
                Console.WriteLine($"{set.ID} - {set.Name} - ${set.Price}");
            }

            Console.WriteLine("------------------");
        }

        // Function to add a dish to the menu

        public void AddDish(string name, decimal price)
        {
            List<Ingredient> dishIngredients = new List<Ingredient>();

            while (true)
            {
                Console.Write("Enter ingredient name (or type 'done' to finish): ");
                string ingredientName = Console.ReadLine();

                if (ingredientName.ToLower() == "done")
                    break;

                Console.Write("Enter ingredient calories: ");
                decimal ingredientCalories;
                while (!decimal.TryParse(Console.ReadLine(), out ingredientCalories) || ingredientCalories <= 0)
                {
                    Console.WriteLine("Invalid calorie input. Please enter a positive number.");
                    Console.Write("Enter ingredient calories: ");
                }

                Console.Write("Enter ingredient price: ");
                decimal ingredientPrice;
                while (!decimal.TryParse(Console.ReadLine(), out ingredientPrice) || ingredientPrice <= 0)
                {
                    Console.WriteLine("Invalid price input. Please enter a positive number.");
                    Console.Write("Enter ingredient price: ");
                }

                Console.Write("Enter ingredient stock quantity: ");
                decimal ingredientStock;
                while (!decimal.TryParse(Console.ReadLine(), out ingredientStock) || ingredientStock <= 0)
                {
                    Console.WriteLine("Invalid stock input. Please enter a positive number.");
                    Console.Write("Enter ingredient stock quantity: ");
                }

                Ingredient newIngredient = new Ingredient(
                    ingredientName,
                    ingredientCalories,
                    ingredientPrice,
                    ingredientStock);

                dishIngredients.Add(newIngredient);
            }

            var newDish = new Dish(name, price, dishIngredients) { ID = _nextID };
            _db.Dishes.Insert(newDish);
            _nextID++;
            Console.WriteLine($"Dish '{name}' added to the menu.");
        }

        // Function to add a set to the menu
        public void AddSet(string name, decimal price, List<Dish> dishes)
        {
            var newSet = new Set(name, price) { ID = _nextID, Dishes = dishes };
            Database.getDatabase().Sets.Insert(newSet);
            _nextID++; // Increment set ID counter
            Console.WriteLine($"Set '{name}' added to the menu.");
        }

        public PayableComponent? ChooseItem(int choice)
        {
            // Check if the choice is for a Dish or a Set
            if (choice > 0)
            {
                // Find Dish by ID
                var dish = Database.getDatabase().Dishes.FindOne(x => x.ID == choice);
                if (dish != null)
                {
                    return dish;
                }

                // Find Set by ID
                var set = Database.getDatabase().Sets.FindOne(x => x.ID == choice);
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