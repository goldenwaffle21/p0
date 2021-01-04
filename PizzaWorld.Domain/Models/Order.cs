using PizzaWorld.Domain.Abstracts;
using PizzaWorld.Domain.Factories;
using System;
using System.Collections.Generic;

namespace PizzaWorld.Domain.Models
{
    public class Order : AEntity
    {
        private GenericPizzaFactory _pizzaFactory = new GenericPizzaFactory();

        public List<APizzaModel> Pizzas {get;set;}

        public bool paid = false;
        public string status = "not yet delivered";
        public double tprice = 0;

        public Order()
        {
            SelectPizzas();
            PrintOrder();
            Pay();
        }

        public void SelectPizzas()
        {
            int i = 1;
            bool done = false;
            while (done == false)
            {
                Console.WriteLine("Select pizza #"+i,
                    "Cheese",
                    "Hawaiian (ham & pineapple, with tomato sauce)",
                    "Meat (peperoni & sausage, with tomato sauce)",
                    "Pesto (artichoke hearts, feta, garlic, & mushrooms, with pesto sauce",
                    "Custom (choose your own ingredients",
                    "",
                    "Please type the name of the type of pizza you want (you will be able to modify it in the next step, if you want to).",
                    "Or, type 'done' if you've already selected all the pizzas you want.");
                string input = Console.ReadLine().ToLower().Trim();
                if (input == "done") {break;}
                else if (input.Contains("cheese")) {MakeCheesePizza();}
                else if (input.Contains("hawaiian")) {MakeHawaiianPizza();}
                else if (input.Contains("meat")) {MakeMeatPizza();}
                else if (input.Contains("pesto")) {MakePestoPizza();}
                else if (input.Contains("custom")) {MakeCustomPizza(new CheesePizza());}
                else
                {
                    Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                    continue;
                }
                PrintPizza(-1);    //displays the most recently created pizza
                CustomOrNot(Pizzas[-1]);
                i += NumberOf(Pizzas[-1]);    //This also adds those pizzas to the list
                PrintOrder();
                Console.WriteLine("",
                    "Add more pizzas? (Y/N)");
                string s = Console.ReadLine().ToLower().Trim();
                if (s == "n" | s == "no") {done = true;}
            }
        }

        public void MakeCheesePizza()
        {
            Pizzas.Add(_pizzaFactory.Make<CheesePizza>());
        }
        public void MakeMeatPizza()
        {
            Pizzas.Add(_pizzaFactory.Make<MeatPizza>());
        }
        public void MakeHawaiianPizza()
        {
            Pizzas.Add(_pizzaFactory.Make<HawaiianPizza>());
        }
        public void MakePestoPizza()
        {
            Pizzas.Add(_pizzaFactory.Make<PestoPizza>());
        }
        public void MakeCustomPizza(APizzaModel p)
        {
            APizzaModel custom = new CustomPizza(p);
            Pizzas.Add(custom);
            Console.WriteLine($"Your {custom.Name} pizza is ready for order:");
            PrintPizza(-1);
            Console.WriteLine("Would you like to modify this pizza further? (Y/N)");
            if (CustomPizza.YesNo())
            {
                Pizzas.Remove(Pizzas[-1]);
                MakeCustomPizza(custom);
            }
        }

        public void PrintPizza(int i)
        {
            APizzaModel p = Pizzas[i];
            Console.WriteLine("You've selected a "+p.Name+" pizza",
                "Size: "+p.Size,
                "Crust: "+p.Crust,
                "Sauce: "+p.Sauce,
                "Topings: "+p.ToppingsString());
        }
        public void CustomOrNot(APizzaModel p)
        {
            Console.WriteLine("Would you like to customize this pizza? (Y/N)");
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "y" | s == "yes")
            {
                Pizzas.RemoveAt(-1);    //remove the unmodified pizza from the order
                MakeCustomPizza(p);
            }
            else if (s != "n" & s != "no")
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                CustomOrNot(p);
            }
        }
        public int NumberOf(APizzaModel p)
        {
            PrintPizza(-1);
            Console.WriteLine("How many of this pizza would you like to order? (Please type the number)");
            if (!int.TryParse(Console.ReadLine(), out int input) | input<0)
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                NumberOf(p);
            }
            if (input == 0)    //If the customer didn't mean to order that pizza
            {
                Pizzas.RemoveAt(-1);
            }
            else
            {
                for (int j=1;j<input;j++)
                {
                    Pizzas.Add(p);
                }
            }
            return input;
        }
        public void PrintOrder()
        {
            Console.WriteLine("Your current order is:");
                foreach (APizzaModel p in Pizzas)
                {
                    Console.WriteLine(p.Size+" "+p.Name+" - "+p.Price);
                    tprice += p.Price;
                }
                Console.WriteLine("Total Price: $"+tprice);
        }

        public void Pay()
        {
            Console.WriteLine("",
                "Pay for order now? (Y/N)");
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "y" | s == "yes") {paid = true;}
            if (s == "n" | s == "no") {paid = false;}
            else 
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                Pay();
            }
        }
    }
}
