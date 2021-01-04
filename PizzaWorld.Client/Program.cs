using PizzaWorld.Domain.Models;
using System;
using System.Linq;

namespace PizzaWorld.Client
{
    class Program
    {
        //private static readonly ClientSingleton _client = ClientSingleton.Instance;
        private static SqlClient _sql = new SqlClient();
        static User u = null;

        static void Main(string[] args)
        {
            UserView();
        }

        static void PrintAllStores()
        {
            foreach(var store in _sql.ReadStores())
            {
                System.Console.WriteLine(store);
            }
        }

        static int NewOrModify()
        {
            Console.WriteLine("(To create a new order, please type 1)",
                "(To cancel a previous order, please type 2",
                "(To modify a previous order, please type 3)",
                "(To see all previous orders, please type 4");
            if (int.TryParse(Console.ReadLine(), out int input) & input<0 & input>5)
            {return input;}
            else
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                NewOrModify();
            }
            return input;
        }

        static void UserView()
        {
            if (u==null) {UserEntry();}
            Console.WriteLine("Welcome to PizzaCo!", 
            "How may we help you today?");
            int i = NewOrModify();
            if (i==1)    //Create a new order
            {
                Console.WriteLine("What store would you like to order from?");
                PrintAllStores();
                u.SelectedStore = _sql.SelectStore();
                u.SelectedStore.CreateOrder();
                Order o = u.SelectedStore.Orders.Last();
                u.Orders.Add(o);
                Payment(u,o);
            }
            else if (i==2 | i==3)    //Modify/delete an old order
            {
                Console.WriteLine("What store was the order placed at?");
                PrintAllStores();
                u.SelectedStore = _sql.SelectStore();
                if (i==3)    //modify
                {
                    Console.WriteLine("What order would you like to modify?",
                        "(Orders are displayed oldest to most recent)",
                        "(You cannot modify orders that have already been delivered; they are not on the list)");
                    u.PrintUndeliveredOrders();
                    Order o = u.SelectOrder();
                    o = u.SelectedStore.ModifyOrder(o,u);
                    Payment(u,o);
                }
                else    //delete
                {
                    Console.WriteLine("What order would you like to cancel?",
                        "(Orders are displayed oldest to most recent)",
                        "(You cannot cancel orders that have already been delivered; they are not on the list)");
                    u.PrintUndeliveredOrders();
                    Order o = u.SelectOrder();
                    u.SelectedStore.DeleteOrder(o,u);
                }
            }
            else if (i==4)    //Display all previous orders
            {
                u.PrintOrders();
                Console.WriteLine("Would you also like to make, modify, or cancel an order? (Y/N)");
                string s = Console.ReadLine().Trim().ToLower();
                if (s=="y" | s=="yes") {UserView();}
                while (s != "y" & s != "yes")
                {
                    if (s=="y" | s=="yes") {UserView();}
                    else if (s == "n" | s == "no") {break;}
                    else
                    {
                        Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                        s = Console.ReadLine().Trim().ToLower();
                    }
                }
            }
            else    //If the input was invalid
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                NewOrModify();
            }

            _sql.Update();
            Console.WriteLine("Thank you for using PizzaCo Online!",
                "Is there more you would like to do on our site?");
            if (YesNo()) {UserView();}
            else {Console.WriteLine("Have a lovely day!");}
        }

        static void Payment(User u, Order o)
        {
            if (u.Orders.Last().paid)
            {
                Console.WriteLine("Your order has been successfully placed!",
                    "Thank you for shopping using PizzaCo (Online); your order will be delivered shortly!");
            }
            else
            {
                Console.WriteLine("We see that you have elected not to pay yet.",
                    "Do you wish to modify your order first? (Y/N)");
                string s = Console.ReadLine();
                if (s == "y" | s == "yes")
                {
                    u.SelectedStore.ModifyOrder(o,u);
                }
                if (s == "n" | s == "no") 
                {
                    Console.WriteLine("Are you sure you don't wish to pay?",
                        "Refusal to pay will result in your order being cancelled. (Pay/Cancel)");
                    while (s != "pay")
                    //Normally, I'd use a recursive function to account for an invalid user input.
                    //That won't work here, so I'm using a while loop.
                    {
                        s = Console.ReadLine().ToLower().Trim();
                        if (s == "cancel") 
                        {
                            Console.WriteLine("Your order has been cancelled. PizzaCo apologizes for the inconvenience.");
                            u.Orders.Remove(o);
                            u.SelectedStore.Orders.Remove(o);
                            break;
                        }
                        else if (s != "pay")
                        {
                            Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                        }
                    }
                    if (u.Orders.Contains(o))    //if the order wasn't cancelled, and thus they chose "Pay"
                    {
                        o.paid = true;
                        Console.WriteLine("Your order has been successfully placed!",
                            "Thank you for shopping using PizzaCo (Online); your order will be delivered shortly!");
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                    Payment(u,o);
                }
            }
        }

        private static void UserEntry()
        {
            Console.WriteLine("Do you already have a PizzaCo Online account, or would you like to create one?",
                "(To log in to an existing account, please type '1')",
                "(To create a new account, please type '2')",
                "(To skip this process and proceed under a temporary account, please type '3')");
            if (int.TryParse(Console.ReadLine(), out int input) & input<0 & input>4)
            {
                if (input==1) {UserLogIn();}
                else if (input==2) {u = CreateUser(false);}
                else {u = CreateUser(true);}
            }
            else
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                UserEntry();
            }
        }

        public static User CreateUser(bool temporary)
        {
            User n = new User();
            if (temporary)
            {
                n.temporary = true;
            }
            else
            {
                Console.WriteLine("Please input your desired username");
                string s = Console.ReadLine();
                Console.WriteLine("The username you typed is: "+s,
                    "Is this acceptable? (Y/N)");
                if (YesNo())
                {
                    n.Name = s;
                    Console.WriteLine("Thank you for creating an account with us!",
                        $"Your User ID is {n.Id} and your Username is {s}");
                }
                else {CreateUser(false);}
            }
            return n;
        }

        private static void UserLogIn()
        {
            Console.WriteLine("User ID - Username");
            foreach (User u1 in _sql.ReadUsers())
            {
                Console.WriteLine(u1.Id+" - "+u1.Name);
            }
            Console.WriteLine("Please type your User ID");
            u = _sql.SelectUser();
            Console.WriteLine("Welcome, "+u.Name+"!");
        }

        public static bool YesNo()    //Wish I'd made this earlier; would have saved some typing.
        {
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "y" | s == "yes") {return true;}
            if (s == "n" | s == "no") {return false;}
            else 
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                return YesNo();
            }
        }
    }
}
