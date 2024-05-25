using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // CCInvoice class for credit card invoices
    public class CCInvoice : Invoice
    {
        public CCInvoice(decimal payment_debt, decimal payment_amount, Customer customer) : base(payment_debt, payment_amount, customer)
        {
        }

        public override string GetInformation(decimal payment_debt, decimal payment_amount, Customer customer)
        {
            return "Credit Card Invoice: " + payment_debt + " " + payment_amount + " " + customer.Name;
        }
    }
}

            