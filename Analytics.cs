using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Analytics class for generating reports
    public class Analytics : InvoicePrinter
    {
        private Database _db;

        public Analytics(Database db) : base()
        {
            _db = db;
        }

        public override string PrintInvoice()
        {
            // Implementation for printing analytics from database
            return "";
        }

        public void HandleAnalytics()
        {
            Console.WriteLine("\nOrder Analytics:");
            Console.WriteLine("----------------");

            // 1. Fetch all orders from the database
            var allOrders = _db.Orders.FindAll();

            decimal total = 0;

            // 2. Loop through each order and calculate the total
            foreach (var order in allOrders)
            {
                decimal orderTotal = order.CalculateTotal(); // Assuming CalculateTotal exists in Order

                total += orderTotal;
                // 3. Display the order ID and its total price
                Console.WriteLine($"Gross Income ${total}");
            }

            Console.WriteLine(); // Add an extra line for readability
        }
    }
}

            