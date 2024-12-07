﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Administrator class to manage database and menu
    public class Administrator
    {
        private Menu _menu;

        public Administrator(Menu menu) // Pass menu as parameter
        {
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
                try
                {
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
                            Analytics analytics = new Analytics();
                            Invoice invoice = analytics.PrintInvoice();
                            Console.WriteLine(invoice.GetInformation());
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
                catch (Exception _)
                {
                    Console.WriteLine("Please enter a number");
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
                try
                {
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
                                    var chosenDish = Database.getDatabase().Dishes.FindOne(x => x.ID == dishId);
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
                catch (Exception _)
                {
                    Console.WriteLine("Invalid input. Please try again");
                    return;
                }
            }
        }

        private void Stocktake()
        {
            while (true)
            {
                Console.WriteLine("\nStocktake");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Print Stocks");
                Console.WriteLine("2. Add Ingredient");
                Console.WriteLine("3. Update Ingredient");
                Console.WriteLine("4. Back to Admin Menu");
                Console.WriteLine("------------------");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            PrintStocks();
                            break;
                        case 2:
                            AddIngredient();
                            break;
                        case 3:
                            UpdateIngredient();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                }
                catch (Exception _)
                {
                    Console.WriteLine("Invalid input. Please try again");
                    return;
                }
            }
        }

        private void PrintStocks()
        {
            // Implement stocktake logic (e.g., display current stock of ingredients)
            // Get all ingredients from the database
            var ingredients = Database.getDatabase().Ingredients.FindAll();
            Console.WriteLine("Stocktake:");
            Console.WriteLine("----------");
            foreach (Ingredient ingredient in ingredients)
            {
                Console.WriteLine($"Ingredient: {ingredient.Name}");
                Console.WriteLine($"Stock: {ingredient.Stock}");
                Console.WriteLine("-----");
            }
        }

        private void AddIngredient()
        {
            Console.Write("Enter ingredient name: ");
            string ingredientName = Console.ReadLine();
            Console.Write("Enter ingredient stock: ");
            decimal ingredientStock = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter ingredient calories: ");
            decimal ingredientCalories = decimal.Parse(Console.ReadLine());
            Console.Write("Enter ingredient price: ");
            decimal ingredientPrice = decimal.Parse(Console.ReadLine());

            Ingredient newIngredient = new Ingredient(ingredientName, ingredientCalories, ingredientPrice, ingredientStock);
            Console.WriteLine(Database.getDatabase().CheckStock(newIngredient));
            if (Database.getDatabase().CheckStock(newIngredient) > -1)
            {
                Console.WriteLine($"Ingredient '{ingredientName}' already exists in stock.");
                return;
            }
            Database.getDatabase().Ingredients.Insert(newIngredient);
            Console.WriteLine($"Ingredient '{ingredientName}' added to stock.");
        }

        private void UpdateIngredient()
        {
            Console.Write("Enter ingredient name: ");
            string updateIngredientName = Console.ReadLine();
            Console.Write("Enter ingredient stock: ");
            decimal newIngredientStock = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter ingredient calories: ");
            decimal newIngredientCalories = decimal.Parse(Console.ReadLine());
            Console.Write("Enter ingredient price: ");
            decimal newIngredientPrice = decimal.Parse(Console.ReadLine());

            Ingredient updateIngredient = new Ingredient(updateIngredientName, newIngredientCalories, newIngredientPrice, newIngredientStock);
            Database.getDatabase().Update(updateIngredient);
            Console.WriteLine($"Information of ingredient '{updateIngredientName}' updated.");
        }

        private void ViewOrders()
        {
            Console.WriteLine("\nOrders:");
            Console.WriteLine("-------");

            var orders = Database.getDatabase().Orders.FindAll();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.ID}");
                Console.WriteLine($"Customer ID: {order.CustomerId}"); // Assuming you have CustomerId in Order
                Console.WriteLine($"Order status: {order.Completed}");

                // Fetch customer details for display (if needed)
                var customer = Database.getDatabase().Customers.FindById(order.CustomerId);
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

            bool doneMarkingOrders = false;

            while (!doneMarkingOrders)
            {
                Console.WriteLine("Enter the ID of the order to mark as completed (or 0 to finish):");
                decimal orderId = decimal.Parse(Console.ReadLine());

                if (orderId == 0)
                {
                    doneMarkingOrders = true;
                }
                else
                {
                    var chosenOrder = Database.getDatabase().Orders.FindOne(x => x.ID == orderId);
                    if (chosenOrder != null)
                    {
                        chosenOrder.Completed = true; // Set Completed to true

                        // Update the order in the database
                        Database.getDatabase().Orders.Update(chosenOrder);

                        Console.WriteLine($"Order '{chosenOrder.ID}' is completed.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid order ID. Please try again.");
                    }
                }
            }
        }

        private void ViewCustomers()
        {
            Console.WriteLine("\nCustomers:");
            Console.WriteLine("----------");

            var customers = Database.getDatabase().Customers.FindAll();
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.Name}");
                Console.WriteLine($"Phone: {customer.Phone}");
                Console.WriteLine($"Email: {customer.Email}");
            }

            bool doneUpdating = false;

            while (!doneUpdating)
            {
                Console.WriteLine();
                Console.WriteLine("Enter the name of customer you want to update: (or 0 to finish):");
                string chosenName = Console.ReadLine();

                if (chosenName == "0")
                {
                    return;
                }
                var chosenCustomer = Database.getDatabase().Customers.FindOne(x => x.Name == chosenName);

                if (chosenCustomer == null)
                {
                    Console.WriteLine("Invalid customer's Name. Please try again.");
                }
                else
                {
                    Console.WriteLine($"1: Phone");
                    Console.WriteLine($"2: Email");
                    Console.WriteLine("Enter (or 0 to finish):");
                    decimal option = decimal.Parse(Console.ReadLine());

                    if (option == 1)
                    {
                        Console.WriteLine("Enter new phone");
                        string newPhone = Console.ReadLine();
                    }
                    else if (option == 2)
                    {
                        Console.WriteLine("Enter new email");
                        string newEmail = Console.ReadLine();
                    }
                    else
                    {
                        doneUpdating = true;
                    }
                    Database.getDatabase().Customers.Update(chosenCustomer);
                }
            }
        }
    }
}
