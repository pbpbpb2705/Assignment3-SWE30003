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
        private Database _db;

        // Add an ID property to the Customer class
        public ObjectId Id { get; set; }

        // Parameterless constructor (required by LiteDB)
        public Customer()
        {
            // You can initialize properties to default values if needed
        }

        public Customer(string name, string phone, string email, Database db)
        {
            Name = name;
            Phone = phone;
            Email = email;
            _db = db;
            Id = ObjectId.NewObjectId(); // Set a new ObjectId here!
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
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Order Food
                        Menu menu = new Menu(_db); // Create menu instance
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
        }

        // Place an order
        public void PlaceOrder(Menu menu)
        {
            // Display menu to terminal
            menu.DisplayMenu();

            // Create a list of PayableComponent
            var order = new Order(DateTime.Now.Ticks, this);

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
            _db.Orders.Insert(order);
        }

        // Get latest order
        public Order GetLatestOrder()
        {
            return _db.Orders.FindOne(x => x.Customer.Name == Name);
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
                    _db.Reservations.Insert(reservation);

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
            string feedbackText = Console.ReadLine();

            // Create a new Feedback object
            Feedback feedback = new Feedback(feedbackText);

            // 1. Find the customer document
            var customer = _db.Customers.FindById(Id);

            // 2. Update the feedback property
            if (customer != null)
            {
                customer.Feedback = feedback;

                // 3. Update the document in the database
                _db.Customers.Update(customer);

                Console.WriteLine("Thank you for your feedback!");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }
}
