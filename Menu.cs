using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Menu
    {
        private Database _db;
        public string Name { get; set; }
        public string Description { get; set; }
        public Menu() 
        { 

        }

        public void DisplayMenu()

        {
            _db.DisplayMenu();
        }

        public PayableComponent ChooseItem(int choice)
        {
            return;
        }

    }
}
