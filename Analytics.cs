using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Assignment3
{
    // Analytics class for generating reports
    public class Analytics : InvoicePrinter
    {

        public Analytics() : base()
        {

        }

        public override string PrintInvoice()
        {
            string invoice = "Analytics Report\n";
            invoice += "----------\n";
            //Get all orders from the database
            IEnumerable<Order> orders = Database.getDatabase().Orders.FindAll();
            int totalOrders = 0;
            decimal totalRevenue = 0;

            foreach (Order order in orders)
            {
                order.Items.ForEach(item => totalRevenue += item.Price);
                totalOrders++;
            }

            invoice += "Total Orders: " + totalOrders + "\n";
            invoice += "Total Revenue: " + totalRevenue + "\n";
            return invoice;
        }
    }
}

            