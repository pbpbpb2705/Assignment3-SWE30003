using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Assignment3
{
    // Analytics class for generating reports
    public class Analytics : InvoicePrinter
    {

        public Analytics() : base()
        {

        }

        public override Invoice PrintInvoice()
        {
            Console.WriteLine("Analytics Report\n----------\n");

            Invoice invoice = new SalesInvoice();
            return invoice;
        }

    }
}

            