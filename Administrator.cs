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
                Console.WriteLine("4. View Orders"); // New option
                Console.WriteLine("5. View Customers"); // New option
                Console.WriteLine("6. Back to Main Menu"); // Shifted option number
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
                        // View Orders
                        ViewOrders();
                        break;
                    case 5:
                        // View Customers
                        ViewCustomers();
                        break;
                    case 6:
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
            Console.WriteLine("\nStocktake:");
            Console.WriteLine("----------");

            // Get all unique ingredients used in dishes (adjust if needed)
            List<Ingredient> allIngredients = _db.Dishes.FindAll()
                .SelectMany(dish => dish.Ingredients) // Flatten the ingredient lists
                .DistinctBy(i => i.Name) // Use DistinctBy to avoid duplicates (if needed)
                .ToList();

            // Display stock information for each ingredient
            foreach (var ingredient in allIngredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Stock} in stock");
            }

            Console.WriteLine(); // Add an extra line for readability
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

        private void ViewOrders()
        {
            Console.WriteLine("\nOrders:");
            Console.WriteLine("-------");

            var orders = _db.Orders.FindAll();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.ID}");
                Console.WriteLine($"Customer ID: {order.CustomerId}"); // Assuming you have CustomerId in Order

                // Fetch customer details for display (if needed)
                var customer = _db.Customers.FindById(order.CustomerId);
                if (customer != null)
                {
                    Console.WriteLine($"Customer Name: {customer.Name}");
                }
                
                Console.WriteLine("Items:");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"- {item.Name} - ${item.Price}");
                }
                Console.WriteLine($"Total: ${order.CalculateTotal()}");
                Console.WriteLine();
            }
        }

        private void ViewCustomers()
        {
            Console.WriteLine("\nCustomers:");
            Console.WriteLine("----------");

            var customers = _db.Customers.FindAll();
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.Name}");
                Console.WriteLine($"Phone: {customer.Phone}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine();
            }
        }
    }
}
