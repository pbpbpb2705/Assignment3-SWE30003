using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Analytics : InvoicePrinter
    {
        private Database _db;
        public Analytics(Database db) : base()
        {
            _db = db
        }
        public override string PrintInvoice()
        {
            //print analytics from database
            
        }
    }
}

            