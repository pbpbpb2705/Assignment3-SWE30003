using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // CashInvoice class for cash invoices
    public class CashInvoice : Invoice
    {
        public CashInvoice(decimal payment_debt, decimal payment_amount, Customer customer) : base(payment_debt, payment_amount, customer)
        {
        }

        public override string GetInformation(decimal payment_debt, decimal payment_amount, Customer customer)
        {
            return "Cash Invoice: " + payment_debt + " " + payment_amount + " " + customer.Name;
        }
    }
}

            