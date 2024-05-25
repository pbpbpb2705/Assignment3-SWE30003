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
            var database = Database.getDatabase(new LiteDatabase("restaurant.db"));
            var menu = new Menu(database); // Create menu instance
            var administrator = new Administrator(database, menu); // Create admin instance with menu reference

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
                        // Customer Mode
                        Console.Write("Enter your name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter your phone number: ");
                        string phone = Console.ReadLine();
                        Console.Write("Enter your email: ");
                        string email = Console.ReadLine();

                        Customer customer = new Customer(name, phone, email, database);
                        database.Customers.Insert(customer); // Add customer to database

                        customer.CustomerMode();
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