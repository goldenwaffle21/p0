using PizzaWorld.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaWorld.Domain.Models
{
    public class CustomPizza : APizzaModel
    {
        public CustomPizza() {}
        public CustomPizza(APizzaModel p)
        {
            AddName(p);
            AddSize(p);
            AddCrust(p);
            AddSauce(p);
            AddToppings(p);
            CalculatePrice();
        }
        protected void AddName(APizzaModel p)
        {
            if (p.Name == "Cheese")
            {
                Name = "Custom";
            }
            else if (p.Name.Contains("Custom"))
            {
                Name = p.Name;
                //This accounts for modifying a custom pizza a second time
            }
            else
            {
                Name = "Custom " + p.Name;
            }
        }
        protected void AddCrust(APizzaModel p)
        {
            Crust = p.Crust;
            if (CustomizeOrNot("crust",Crust))
            {
                CustomizeCrust();
            }
        }
        protected void AddSize(APizzaModel p)
        {
            Size = p.Size;
            if (CustomizeOrNot("size",Size))
            {
                CustomizeSize();
            }
        }
        protected void AddSauce(APizzaModel p)
        {
            Sauce = p.Sauce;
            if (CustomizeOrNot("sauce",Sauce))
            {
                CustomizeSauce();
            }
        }
        protected void AddToppings(APizzaModel p)
        {
            Toppings = p.Toppings;
            //Need a prompt specific to toppings, to account for plurals
            Console.WriteLine($"\nYou currently have the following toppings: \n{ToppingsString()} \nWould you like to customize this? (Y/N)");
            if (YesNo())
            {
                string pt = ToppingsString();
                Toppings = new List<Topping>();
                bool done = false;
                while (!done)
                {
                    Console.WriteLine($"\nPrevious Toppings: {pt} \nCurrent Toppings: {ToppingsString()} \nAdd another topping? (Y/N)");
                    if (YesNo())
                    {
                        done = AddATopping();
                    }
                    else {done = true;}
                }
            }
        }

        //This marks the point where I finally smartened up and created methods for printing to
        //and reading from the console for Yes/No questions.
        protected bool CustomizeOrNot(string cat,string value)
        {
            Console.WriteLine($"\nThe {cat} is currently: {value} \nWould you like to customize this? (Y/N)");
            return YesNo();
        }

        public static bool YesNo()    //Really, this should be in Program, but I'm too lazy to move it now.
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

        private void CustomizeCrust()
        {
            Console.WriteLine("\nWhat type of crust would you like? \n(regular     deep dish     stuffed     thin)");
            string s = Console.ReadLine().ToLower().Trim();
            if (s == "regular") {Crust = "regular";}
            else if (s=="deep dish" | s=="deepdish" | s=="deep-dish") {Crust = "deep dish";}
            else if (s == "stuffed") {Crust = "stuffed";}
            else if (s == "thin") {Crust = "thin";}
            else 
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                CustomizeCrust();
            }
        }

        private void CustomizeSize()
        {
            Console.WriteLine("\nWhat size pizza would you like? \n(small     medium     large)");
            string s = Console.ReadLine().ToLower().Trim();
            if (s == "small") {Size = s;}
            else if (s == "medium") {Size = s;}
            else if (s == "large") {Size = s;}
            else 
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                CustomizeSize();
            }
        }
        private void CustomizeSauce()
        {
            Console.WriteLine("\nWhat type of sauce would you like? \n(alfredo     barbeque     pesto     tomato)");
            string s = Console.ReadLine().ToLower().Trim();
            if (s == "alfredo") {Sauce = s;}
            else if (s == "barbeque" | s == "bbq") {Sauce = s;}
            else if (s == "pesto") {Sauce = s;}
            else if (s == "tomato") {Sauce = s;}
            else 
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                CustomizeSauce();
            }
        }

        private bool AddATopping()
        {
            Console.WriteLine("\nEach topping adds $0.50 to your order. What topping would you like to add? (no special characters, please!) \n(If you already have all the toppings you want, type 'done')");
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "done") {return true;}
            //Check to make sure this isn't a duplicate
            foreach (Topping t in Toppings)
            {
                if (t.Name != "" && (s.Contains(t.Name) | t.Name.Contains(s)))
                {
                    int i;
                    Console.WriteLine($"\nYou typed '{s}', but the pizza already has '{t.Name}' \nIf you want extra {t.Name}, please type '1'. \nIf you typed {s} in error, please type '2'.");
                    if (t.Name != s)
                    {
                        Console.WriteLine($"If you do want both {s} and {t.Name}, please type '3'.");
                        i = DuplicateQuery(3);
                        //You don't get the 3rd option if you've typed exactly the same ingredient;
                        //that might screw up the table, since it uses Name as the primary key
                        //for Toppings.
                    }
                    else {i = DuplicateQuery(2);}
                    if (i == 1)
                    {
                        if (!s.Contains("extra") | t.Name.Contains("extra")) {s = "extra "+t.Name;}
                        Toppings.Remove(t);
                        //The if statement is a little weird, so I'll explain. We want to make the topping
                        //"extra", but what if it was already extra and they wanted even more? If the 
                        //topping was already "extra", this adds another layer; otherwise, it adds either 
                        //a single "extra" or, if they specified some number of extras, that many.
                        
                        break;
                        //modifying Toppings screws up the foreach loop something fierce.
                        //Fortunately, we've already found the duplicate and fixed the problem.
                    }
                    else if (i == 2)
                    {
                        s = null;
                        return false;
                    }
                }
            }
            //We also need to deal with it if the customer inputs "extra" as part of the topping,
            //or even multiple extras (or if we made it extra based on the above).
            int j = s.LastIndexOf("extra");
            while (j>0)
            //Doesn't run if there are no "extra"s (j=-1); otherwise, stops once it reaches the first extra.
            {
                Toppings.Add(new Topping(""));    //Won't interfere with the display, and makes the price calculation work right.
                j = s.LastIndexOf("extra",(j-1));
            }

            if (s != null) {Toppings.Add(new Topping(s));}
            Toppings.OrderBy(top => top.Name);

            return false;
        }

        private int DuplicateQuery(int options)
        {
            if (int.TryParse(Console.ReadLine(),out int i) && i>0 && i<=options) {return i;}
            else
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                return DuplicateQuery(options);
            }
        }
    }
}