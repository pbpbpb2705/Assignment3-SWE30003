using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Administrator class to manage database and menu
    public class Administrator
    {
        private Database _db;
        private Menu _menu;

        public Administrator(Database db, Menu menu) // Pass menu as parameter
        {
            _db = db;
            _menu = menu;
        }

        public void AdminMode()
        {
            while (true)
            {
                Console.WriteLine("\nAdministrator Mode");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Manage Menu");
                Console.WriteLine("2. Stocktake");
                Console.WriteLine("3. Analytics");
                Console.WriteLine("4. Back to Main Menu");
                Console.WriteLine("------------------");

                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Manage Menu
                        ManageMenu();
                        break;
                    case 2:
                        // Stocktake
                        Stocktake();
                        break;
                    case 3:
                        // Analytics
                        Analytics analytics = new Analytics(_db);
                        analytics.HandleAnalytics();
                        break;
                    case 4:
                        // Back to Main Menu
                        return; // Exit Admin Mode
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ManageMenu()
        {
            while (true)
            {
                Console.WriteLine("\nManage Menu");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Add Dish");
                Console.WriteLine("2. Add Set");
                Console.WriteLine("3. Display Menu");
                Console.WriteLine("4. Back to Admin Menu");
                Console.WriteLine("------------------");

                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter dish name: ");
                        string dishName = Console.ReadLine();
                        Console.Write("Enter dish price: ");
                        decimal dishPrice = decimal.Parse(Console.ReadLine());
                        _menu.AddDish(dishName, dishPrice); // Use _menu to add dish
                        break;

                    case 2:
                        Console.Write("Enter set name: ");
                        string setName = Console.ReadLine();
                        Console.Write("Enter set price: ");
                        decimal setPrice = decimal.Parse(Console.ReadLine());

                        // Loop to choose dishes for the set
                        List<Dish> setDishes = new List<Dish>();
                        bool doneAddingDishes = false;
                        while (!doneAddingDishes)
                        {
                            _menu.DisplayMenu(); // Display the menu to show dishes
                            Console.WriteLine("Enter the ID of the dish to add to the set (or 0 to finish):");
                            int dishId = int.Parse(Console.ReadLine());

                            if (dishId == 0)
                            {
                                doneAddingDishes = true;
                            }
                            else
                            {
                                var chosenDish = _db.Dishes.FindOne(x => x.ID == dishId);
                                if (chosenDish != null)
                                {
                                    setDishes.Add(chosenDish);
                                    Console.WriteLine($"Dish '{chosenDish.Name}' added to the set.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid dish ID. Please try again.");
                                }
                            }
                        }

                        _menu.AddSet(setName, setPrice, setDishes);
                        break;

                    case 3:
                        _menu.DisplayMenu();
                        break;
                    case 4:
                        return; // Back to Admin Menu
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void Stocktake()
        {
            // Implement stocktake logic (e.g., display current stock of ingredients)
            // ...
        }

        // Methods to add, delete, and update data in database
        public void AddDatabase(Database db)
        {
            // ... (Implementation not provided in the original code)
        }

        public void DeleteDatabase(Database db)
        {
            // ... (Implementation not provided in the original code)
        }

        public void UpdateDatabase(Database db)
        {
            // ... (Implementation not provided in the original code)
        }

        // Method to handle analytics
        public void HandleAnalyic(Analytics analytics)
        {
            analytics.HandleAnalytics();
        }
    }
}
