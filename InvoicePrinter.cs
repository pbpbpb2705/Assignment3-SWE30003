using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // InvoicePrinter abstract class
    public abstract class InvoicePrinter
    {
        public InvoicePrinter()
        {
        }

        public abstract Invoice PrintInvoice();
    }
}
