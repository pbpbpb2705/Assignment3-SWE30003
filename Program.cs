using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace Assignment3
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize Database class with LiteDB instance
            var menu = new Menu(); // Create menu instance
            var administrator = new Administrator(menu); // Create admin instance with menu reference

            while (true)
            {
                Console.WriteLine("\nRestaurant Management System");
                Console.WriteLine("--------------------------");
                Console.WriteLine("1. Administrator");
                Console.WriteLine("2. Customer");
                Console.WriteLine("3. Exit");
                Console.WriteLine("--------------------------");

                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Administrator Mode
                        administrator.AdminMode();
                        break;
                    case 2:
                        Console.Write("Enter your name: ");
                        string name = Console.ReadLine();

                        // 1. Check if a customer with the same name exists
                        Customer existingCustomer = database.Customers.FindOne(c => c.Name == name);

                        if (existingCustomer != null)
                        {
                            // 2. Customer exists, use the existing customer object
                            Console.WriteLine($"Welcome back, {existingCustomer.Name}!");
                            existingCustomer.CustomerMode();
                        }
                        else
                        {
                            // 3. Customer doesn't exist, prompt for details and create a new customer
                            Console.Write("Enter your phone number: ");
                            string phone = Console.ReadLine();
                            Console.Write("Enter your email: ");
                            string email = Console.ReadLine();

                            Customer newCustomer = new Customer(name, phone, email, database);
                            database.Customers.Insert(newCustomer);

                            Console.WriteLine($"Welcome, {newCustomer.Name}!");
                            newCustomer.CustomerMode();
                        }

                        break;
                    case 3:
                        Console.WriteLine("Exiting...");
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}