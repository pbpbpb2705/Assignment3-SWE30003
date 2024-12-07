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
        public decimal Payment_debt { get; set; }
        public decimal Payment_amount { get; set; }
        public Customer Customer { get; set; }
        public CashInvoice(decimal payment_debt, decimal payment_amount, Customer customer) : base()
        {
            Payment_debt = payment_debt;
            Payment_amount = payment_amount;
            Customer = customer;
        }

        public override string GetInformation()
        {
            return "Cash Invoice: Payment debt: " + Payment_debt + "$, Payment amount: " + Payment_amount + "$, Customer's name: " + Customer.Name;
        }
    }
}

            