using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // PayableComponent abstract class for items that can be paid for
    public abstract class PayableComponent
    {
        public int ID { get; set; } // Add ID property
        public string Name { get; set; }
        public decimal Price { get; set; }

        public PayableComponent(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public virtual decimal CalculatePayment(int count)
        {
            return Price * count;
        }
    }
}
