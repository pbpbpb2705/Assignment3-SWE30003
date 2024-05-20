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
        public Payment(Order order) : base()
        {
            _order = order
        }
        public override string PrintInvoice()
        {
            string invoice = "";
            invoice += "Invoice\n";
            invoice += "Order ID: " + _order.ID + "\n";
            invoice += "Customer: " + _order.Customer.Name + "\n";
            invoice += "Phone: " + _order.Customer.Phone + "\n";
            invoice += "Email: " + _order.Customer.Email + "\n";
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

            