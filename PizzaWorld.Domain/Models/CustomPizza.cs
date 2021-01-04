using PizzaWorld.Domain.Abstracts;
using System;
using System.Collections.Generic;

namespace PizzaWorld.Domain.Models
{
    public class CustomPizza : APizzaModel
    {
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
            Size = p.Size;
            if (CustomizeOrNot("sauce",Sauce))
            {
                CustomizeSauce();
            }
        }
        protected void AddToppings(APizzaModel p)
        {
            Toppings = p.Toppings;
            //Need a prompt specific to toppings, to account for plurals
            Console.WriteLine($"You currently have the following toppings:",
                ToppingsString(),
                "Would you like to customize this? (Y/N)");
            if (YesNo())
            {
                string pt = ToppingsString();
                Toppings = new List<string>();
                bool done = false;
                while (!done)
                {
                    Console.WriteLine("Previous Toppings: "+pt,
                        "Current Toppings: "+ToppingsString(),
                        "Add another topping? (Y/N)");
                    if (YesNo())
                    {
                        AddATopping();
                    }
                    else {done = true;}
                }
            }
        }

        //This marks the point where I finally smartened up and created methods for printing to
        //and reading from the console for Yes/No questions.
        protected bool CustomizeOrNot(string cat,string value)
        {
            Console.WriteLine($"The {cat} is currently: {value}",
                "Would you like to customize this? (Y/N)");
            return YesNo();
        }

        public static bool YesNo()    //Really, this should be in Program, but I'm too lazy to move it now.
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

        private void CustomizeCrust()
        {
            Console.WriteLine("What type of crust would you like?",
                    "(regular     deep dish     stuffed     thin)");
            string s = Console.ReadLine().ToLower().Trim();
            if (s == "regular") {Crust = "regular";}
            else if (s=="deep dish" | s=="deepdish" | s=="deep-dish") {Crust = "deep dish";}
            else if (s == "stuffed") {Crust = "stuffed";}
            else if (s == "thin") {Crust = "thin";}
            else 
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                CustomizeCrust();
            }
        }

        private void CustomizeSize()
        {
            Console.WriteLine("What size pizza would you like?",
                    "(small     medium     large)");
            string s = Console.ReadLine().ToLower().Trim();
            if (s == "small") {Size = s;}
            else if (s == "medium") {Size = s;}
            else if (s == "large") {Size = s;}
            else 
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                CustomizeSize();
            }
        }
        private void CustomizeSauce()
        {
            Console.WriteLine("What type of sauce would you like?",
                    "(alfredo     barbeque     pesto     tomato)");
            string s = Console.ReadLine().ToLower().Trim();
            if (s == "alfredo") {Sauce = s;}
            else if (s == "barbeque" | s == "bbq") {Sauce = s;}
            else if (s == "pesto") {Sauce = s;}
            else if (s == "tomato") {Sauce = s;}
            else 
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                CustomizeSauce();
            }
        }

        private void AddATopping()
        {
            Console.WriteLine("Each topping adds $0.50 to your order. What topping would you like to add? (no special characters, please!)",
                "(If you already have all the toppings you want, type 'done')");
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "done") {return;}
            //Check to make sure this isn't a duplicate
            foreach (string t in Toppings)
            {
                if (s.Contains(t) | t.Contains(s))
                {
                    Console.WriteLine($"You typed {s}, but the pizza already has {t}",
                        $"If you want extra {t}, please type '1'.",
                        $"If you typed {s} in error, please type '2'.",
                        $"If you do want both {s} and {t}, please type '3'.");
                    int i =DuplicateQuery();
                    if (i == 1)
                    {
                        if (!s.Contains("extra") | t.Contains("extra")) {s = "extra "+t;}
                        Toppings.Remove(t);
                        //The if statement is a little weird, so I'll explain. We want to make the topping
                        //"extra", but what if it was already extra and they wanted even more? If the 
                        //topping was already "extra", this adds another layer; otherwise, it adds either 
                        //a single "extra" or, if they specified some number of extras, that many.
                    }
                    else if (i == 2)
                    {
                        s = null;
                        break;
                    }
                }
            }
            //We also need to deal with it if the customer inputs "extra" as part of the topping,
            //or even multiple extras (or if we made it extra based on the above).
            int j = s.LastIndexOf("extra");
            while (j>-1)
            {
                Toppings.Add("");    //Won't interfere with the display, and makes the price calculation work right.
                j = s.LastIndexOf("extra",j-1);
            }

            if (s != null) {Toppings.Add(s);}
            Toppings.Sort();
        }

        private int DuplicateQuery()
        {
            if (int.TryParse(Console.ReadLine(),out int i) & i>0 & i<4) {return i;}
            else
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                return DuplicateQuery();
            }
        }
    }
}