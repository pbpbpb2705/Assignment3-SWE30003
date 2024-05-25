using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Assignment3
{
    // Order class to represent an order
    public class Order
    {
        public decimal ID { get; set; }

        // Store the Customer's ObjectId directly
        public ObjectId? CustomerId { get; set; }

        public List<PayableComponent> Items { get; set; }

        public Order(decimal id)
        {
            ID = id;
            Items = new List<PayableComponent>();
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
