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

        public Payment(Order order) : base() // Pass the database instance
        {
            _order = order;
        }

        public override Invoice PrintInvoice()
        {
            // Fetch the customer from the database using the CustomerId in the Order
            var customer = Database.getDatabase().Customers.FindById(_order.CustomerId);

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
                invoice += item.Name + " " + item.Price + "$\n";
            }
            invoice += "Total: " + _order.CalculateTotal() + "$\n";
            try
            {
                bool finished = false;
                while (!finished)
                {
                    Console.WriteLine("Default payment method is Credit Card. Do you want to change it? (Y/N)");
                    string paymentMethod = Console.ReadLine();
                    if (paymentMethod == "Y")
                    {
                        CashInvoice receipt = new CashInvoice(_order.CalculateTotal(), _order.CalculateTotal(), customer);
                        invoice += receipt.GetInformation();
                        Console.WriteLine(invoice);
                        return receipt;
                    }
                    else if (paymentMethod == "N")
                    {
                        CCInvoice receipt = new CCInvoice(_order.CalculateTotal(), _order.CalculateTotal(), customer);
                        invoice += receipt.GetInformation();
                        Console.WriteLine(invoice);
                        return receipt;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input. Please try again");
                return new CashInvoice(_order.CalculateTotal(), 0, customer);
            }
            return new CashInvoice(_order.CalculateTotal(), 0, customer);
        }
    }
}

            