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
                System.Console.WriteLine(store.Name);
            }
        }

        static int NewOrModify()
        {
            Console.WriteLine("\n(To create a new order, please type 1) \n(To cancel a previous order, please type 2 \n(To modify a previous order, please type 3) \n(To see all previous orders, please type 4)");
            if (int.TryParse(Console.ReadLine(), out int input) && input>0 && input<5)
            {return input;}
            else
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                return NewOrModify();
            }
        }

        static void UserView()
        {
            Console.WriteLine("\n\nWelcome to PizzaCo!");
            if (u==null) {UserEntry();}
            Console.WriteLine("\nHow may we help you today?");
            int i = NewOrModify();
            if (i==1)    //Create a new order
            {
                Console.WriteLine("\nWhat store would you like to order from?");
                PrintAllStores();
                u.SelectedStore = _sql.SelectStore();
                u.SelectedStore.CreateOrder();
                Order o = u.SelectedStore.Orders.Last();
                u.Orders.Add(o);
                Payment(u,o);
            }
            else if (i==2 | i==3)    //Modify/delete an old order
            {
                Console.WriteLine("\nWhat store was the order placed at?");
                PrintAllStores();
                u.SelectedStore = _sql.SelectStore();
                if (i==3)    //modify
                {
                    Console.WriteLine("\nWhat order would you like to modify? \n(Orders are displayed oldest to most recent) \n(You cannot modify orders that have already been delivered; they are not on the list)");
                    u.PrintUndeliveredOrders();
                    Order o = u.SelectOrder();
                    o = u.SelectedStore.ModifyOrder(o,u);
                    Payment(u,o);
                }
                else    //delete
                {
                    Console.WriteLine("\nWhat order would you like to cancel? \n(Orders are displayed oldest to most recent) \n(You cannot cancel orders that have already been delivered; they are not on the list)");
                    u.PrintUndeliveredOrders();
                    Order o = u.SelectOrder();
                    u.SelectedStore.DeleteOrder(o,u);
                }
            }
            else if (i==4)    //Display all previous orders
            {
                u.PrintOrders();
                Console.WriteLine("\nWould you also like to make, modify, or cancel an order? (Y/N)");
                string s = Console.ReadLine().Trim().ToLower();
                if (s=="y" | s=="yes") {UserView();}
                while (s != "y" & s != "yes")
                {
                    if (s=="y" | s=="yes") {UserView();}
                    else if (s == "n" | s == "no") {break;}
                    else
                    {
                        Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                        s = Console.ReadLine().Trim().ToLower();
                    }
                }
            }
            else    //If the input was invalid
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                NewOrModify();
            }

            //Save this operation to the database and ask if the user wants another operation
            _sql.Update(u.SelectedStore,u);
            Console.WriteLine("\nThank you for using PizzaCo Online! \nIs there more you would like to do on our site? (Y/N)");
            if (YesNo()) {UserView();}
            else {Console.WriteLine("\nHave a lovely day!");}
        }

        static void Payment(User u, Order o)
        {
            if (u.Orders.Last().paid)
            {
                Console.WriteLine("\nYour order has been successfully placed! \nThank you for shopping using PizzaCo (Online); your order will be delivered shortly!");
            }
            else
            {
                Console.WriteLine("\nWe see that you have elected not to pay yet. \nDo you wish to modify your order first? (Y/N)");
                string s = Console.ReadLine();
                if (s == "y" | s == "yes")
                {
                    u.SelectedStore.ModifyOrder(o,u);
                }
                if (s == "n" | s == "no") 
                {
                    Console.WriteLine("\nAre you sure you don't wish to pay? \nRefusal to pay will result in your order being cancelled. (Pay/Cancel)");
                    while (s != "pay")
                    //Normally, I'd use a recursive function to account for an invalid user input.
                    //That won't work here, so I'm using a while loop.
                    {
                        s = Console.ReadLine().ToLower().Trim();
                        if (s == "cancel") 
                        {
                            Console.WriteLine("\nYour order has been cancelled. PizzaCo apologizes for the inconvenience.");
                            u.Orders.Remove(o);
                            u.SelectedStore.Orders.Remove(o);
                            break;
                        }
                        else if (s != "pay")
                        {
                            Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                        }
                    }
                    if (u.Orders.Contains(o))    //if the order wasn't cancelled, and thus they chose "Pay"
                    {
                        o.paid = true;
                        Console.WriteLine("\nYour order has been successfully placed! \nThank you for shopping using PizzaCo (Online); your order will be delivered shortly!");
                    }
                }
                else
                {
                    Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                    Payment(u,o);
                }
            }
        }

        private static void UserEntry()
        {
            Console.WriteLine("\nDo you already have a PizzaCo Online account, or would you like to create one? \n(To log in to an existing account, please type '1') \n(To create a new account, please type '2') \n(To skip this process and proceed under a temporary account, please type '3')");
            if (int.TryParse(Console.ReadLine(), out int input) && input>0 && input<4)
            {
                if (input==1) {UserLogIn();}
                else if (input==2) {u = CreateUser(false);}
                else {u = CreateUser(true);}
            }
            else
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                UserEntry();
            }
        }

        public static User CreateUser(bool temporary)
        {
            User n = new User();
            if (temporary)
            {
                n.temporary = true;
                return n;
            }
            else
            {
                Console.WriteLine("\nPlease input your desired username");
                string s = Console.ReadLine();
                Console.WriteLine("\nThe username you typed is: "+s+"\nIs this acceptable? (Y/N)");
                if (YesNo())
                {
                    n.Name = s;
                    _sql.Save(n);
                    n = _sql.ReadOneUser(s);
                    Console.WriteLine($"\nThank you for creating an account with us! \nYour User ID is {n.Id} and your Username is {s}");
                    return n;
                }
                else {return CreateUser(false);}
            }
        }

        private static void UserLogIn()
        {
            Console.WriteLine("\nUser ID - Username");
            foreach (User u1 in _sql.ReadUsers())
            {
                Console.WriteLine(u1.Id+" - "+u1.Name);
            }
            Console.WriteLine("\nPlease type your User ID");
            u = _sql.SelectUser();
            Console.WriteLine("\nWelcome, "+u.Name+"!");
        }

        public static bool YesNo()    //Wish I'd made this earlier; would have saved some typing.
        {
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "y" | s == "yes") {return true;}
            if (s == "n" | s == "no") {return false;}
            else 
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                return YesNo();
            }
        }
    }
}
