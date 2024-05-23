using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Order
    {
        public decimal ID { get; set; }
        public Customer Customer { get; set; }
        public List<PayableComponent> Items { get; set; } // List to hold items

        public Order(decimal id, Customer customer)
        {
            ID = id;
            Customer = customer;
            Items = new List<PayableComponent>(); // Initialize the list in the constructor
        }

        // Method to add an item to the order
        public void AddItem(PayableComponent item)
        {
            Items.Add(item);
        }

        // Method to calculate total price of the order
        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (PayableComponent item in Items)
            {
                total += item.Price;
            }
            return total;
        }
    }
}
