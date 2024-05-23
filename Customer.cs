using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Payed { get; set; }
        public List<Order> Orders { get; set; }
        public FeedBack Feedback { get; set; }
        public Customer(string name, string phone, string email, List<Order> orders)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Orders = orders;
            Payed = false;
            Feedback = null;
        }
        public string ID { get; set; }
        public void PlaceOrder(Menu menu)
        {
            //display menu to terminal
            menu.DisplayMenu();
            //create a list of PayableComponent
            Order order = new Order();
            bool done = false;
            //add items that the user choose in the terminal to list
            while (done)
            {
                //read the user input
                Console.WriteLine("Enter the item you want to order: ");
                String choice = Console.ReadLine();
                int choiceInt = Int32.Parse(choice);
                if (choiceInt == 0)
                {
                    done = true;
                }
                else 
                {
                    order.Items.Add(menu.ChooseItem(choiceInt));
                }
            }
        }
    }
}
