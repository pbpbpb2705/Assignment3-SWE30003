using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // SalesInvoice class for sales invoices
    public class SalesInvoice : Invoice
    {
        public decimal TotalOrder { get; set; }
        public decimal TotalRevenue { get; set; }
        public SalesInvoice() : base()
        {

        }

        public override string GetInformation()
        {
            //Get all orders from the database
            IEnumerable<Order> orders = Database.getDatabase().Orders.FindAll();
            int totalOrders = 0;
            decimal totalRevenue = 0;

            foreach (Order order in orders)
            {
                order.Items.ForEach(item => totalRevenue += item.Price);
                totalOrders++;
            }

            TotalOrder = totalOrders;
            TotalRevenue = totalRevenue;

            return "Sales Invoice:\nNumber of orders: " + TotalOrder + "\nTotal revenue: " + TotalRevenue + "$";
        }
    }
}

            