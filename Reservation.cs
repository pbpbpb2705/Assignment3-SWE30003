using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal abstract class Reservation
    {
        public string ID { get; set; }
        public Customer Customer { get; set; }
        public int Table { get; set; }
        public DateTime Date { get; set; }
        public Reservation(string id, Customer customer, int table, DateTime date)
        {
            ID = id;
            Customer = customer;
            Date = date;
        }
    }
}
