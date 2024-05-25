using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Payment : InvoicePrinter
    {
        private Order _order;
        private Database _db; // You'll need to pass the Database instance to Payment

        public Payment(Order order, Database db) : base() // Pass the database instance
        {
            _order = order;
            _db = db;
        }

        public override string PrintInvoice()
        {
            // Fetch the customer from the database using the CustomerId in the Order
            var customer = _db.Customers.FindById(_order.CustomerId);

            string invoice = "";
            invoice += "Invoice\n";
            invoice += "Order ID: " + _order.ID + "\n";

            // Check if the customer was found
            if (customer != null)
            {
                invoice += "Customer: " + customer.Name + "\n";
                invoice += "Phone: " + customer.Phone + "\n";
                invoice += "Email: " + customer.Email + "\n";
            }
            else
            {
                invoice += "Customer: Not Found (ID: " + _order.CustomerId + ")\n";
            }

            invoice += "Items:\n";
            foreach (PayableComponent item in _order.Items)
            {
                invoice += item.Name + " " + item.Price + "\n";
            }
            invoice += "Total: " + _order.CalculateTotal() + "\n";
            return invoice;
        }
    }
}

            