using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Invoice abstract class for invoices
    public abstract class Invoice
    {
        public decimal Payment_debt { get; set; }
        public decimal Payment_amount { get; set; }
        public Customer Customer { get; set; }

        public Invoice(decimal payment_debt, decimal payment_amount, Customer customer)
        {
            Payment_debt = payment_debt;
            Payment_amount = payment_amount;
            Customer = customer;
        }

        public abstract string GetInformation(decimal payment_debt, decimal payment_amount, Customer customer);
    }
}
