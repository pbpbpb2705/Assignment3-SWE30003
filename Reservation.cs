using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Reservation class (you need to create this)
    public class Reservation
    {
        public string ID { get; set; }
        public Customer Customer { get; set; }
        public int Table { get; set; } // You might need to implement table assignment logic
        public DateTime Date { get; set; }

        public Reservation(string id, Customer customer, int table, DateTime date)
        {
            ID = id;
            Customer = customer;
            Table = table; // Implement table assignment logic here
            Date = date;
        }
    }
}
