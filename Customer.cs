using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using LiteDB;
using LiteDB.Config;

namespace Assignment3
{
    // Customer class representing a customer
    public class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Payed { get; set; }
        public List<Order> Orders { get; set; }
        public Feedback Feedback { get; set; }

        // Add an ID property to the Customer class
        public ObjectId Id { get; set; }

        // Parameterless constructor (required by LiteDB)
        public Customer()
        {
            Orders = new List<Order>(); // Initialize the list here
        }

        public Customer(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Id = ObjectId.NewObjectId(); // Set a new ObjectId here!
            Orders = new List<Order>(); // Initialize the list here as well
        }

        public void CustomerMode()
        {
            while (true)
            {
                Console.WriteLine("\nCustomer Mode");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Order Food");
                Console.WriteLine("2. Make Reservation");
                Console.WriteLine("3. Give Feedback");
                Console.WriteLine("4. Back to Main Menu");
                Console.WriteLine("------------------");

                Console.Write("Choose an option: ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            // Order Food
                            Menu menu = new Menu(); // Create menu instance
                            PlaceOrder(menu);
                            break;
                        case 2:
                            // Make Reservation
                            MakeReservation();
                            break;
                        case 3:
                            // Give Feedback
                            GiveFeedback();
                            break;
                        case 4:
                            // Back to Main Menu
                            return; // Exit Customer Mode
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

        // Place an order
        public void PlaceOrder(Menu menu)
        {
            // Display menu to terminal
            menu.DisplayMenu();

            var order = new Order(DateTime.Now.Ticks)
            {
                CustomerId = Id // Set the CustomerId to the current customer's Id
            };

            bool done = false;

            // Add items that the user chooses in the terminal to list
            while (!done)
            {
                Console.WriteLine("Enter the item you want to order (or 0 to finish): ");
                string choice = Console.ReadLine();
                if (choice == "0")
                {
                    done = true;
                }
                else
                {
                    // Parse the input into an integer
                    if (int.TryParse(choice, out int choiceInt))
                    {
                        // Choose item from menu
                        var item = menu.ChooseItem(choiceInt);
                        if (item != null)
                        {
                            order.AddItem(item);
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
            }

            // Add order to customer's list
            Orders.Add(order);
            Database.getDatabase().Orders.Insert(order);
        }

        // Get latest order
        public Order GetLatestOrder()
        {
            return Database.getDatabase().Orders.FindOne(x => x.CustomerId == Id); // Use CustomerId to find the order
        }

        private void MakeReservation()
        {
            Console.WriteLine("\nMake Reservation");
            Console.WriteLine("------------------");

            Console.Write("Enter reservation date (YYYY-MM-DD): ");
            DateTime reservationDate;
            if (DateTime.TryParse(Console.ReadLine(), out reservationDate))
            {
                // Check if the date is valid (e.g., not in the past)
                if (reservationDate >= DateTime.Now)
                {
                    Console.Write("Enter number of people: ");
                    int numPeople = int.Parse(Console.ReadLine());

                    // Generate a unique reservation ID
                    string reservationId = Guid.NewGuid().ToString();

                    // Create a new reservation
                    Reservation reservation = new Reservation(reservationId, this, numPeople, reservationDate);

                    // Insert the reservation into the database
                    Database.getDatabase().Reservations.Insert(reservation);

                    Console.WriteLine($"Reservation successfully made. Your reservation ID is: {reservationId}");
                }
                else
                {
                    Console.WriteLine("Invalid date. Please enter a date in the future.");
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
            }
        }

        private void GiveFeedback()
        {
            Console.WriteLine("\nGive Feedback");
            Console.WriteLine("------------------");

            Console.Write("Enter your feedback: ");
            string? feedbackText = Console.ReadLine();
            if (string.IsNullOrEmpty(feedbackText))
            {
                feedbackText = "No feedback provided.";
            }
            // Create a new Feedback object
            Feedback feedback = new Feedback(feedbackText);

            // 1. Find the customer document
            var customer = Database.getDatabase().Customers.FindById(Id);

            // 2. Update the feedback property
            if (customer != null)
            {
                customer.Feedback = feedback;

                // 3. Update the document in the database
                Database.getDatabase().Customers.Update(customer);

                Console.WriteLine("Thank you for your feedback!");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }
}
