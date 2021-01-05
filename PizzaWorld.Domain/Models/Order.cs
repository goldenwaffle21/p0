using PizzaWorld.Domain.Abstracts;
using PizzaWorld.Domain.Factories;
using System;
using System.Collections.Generic;

namespace PizzaWorld.Domain.Models
{
    public class Order : AEntity
    {
        private GenericPizzaFactory _pizzaFactory = new GenericPizzaFactory();

        public List<APizzaModel> Pizzas = new List<APizzaModel>();

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
                Console.WriteLine($"\nSelect pizza #{i}: \n\nCheese \nHawaiian (ham & pineapple, with tomato sauce) \nMeat (peperoni & sausage, with tomato sauce) \nPesto (artichoke hearts, feta, garlic, & mushrooms, with pesto sauce) \nCustom (choose your own ingredients) \n\nPlease type the name of the type of pizza you want (you will be able to modify it in the next step, if you want to). \nOr, type 'done' if you've already selected all the pizzas you want.");
                string input = Console.ReadLine().ToLower().Trim();
                if (input == "done") {break;}
                else if (input.Contains("cheese")) {MakeCheesePizza();}
                else if (input.Contains("hawaiian") | input.Contains("hawaian")) {MakeHawaiianPizza();}
                else if (input.Contains("meat")) {MakeMeatPizza();}
                else if (input.Contains("pesto")) {MakePestoPizza();}
                else if (input.Contains("custom")) {MakeCustomPizza(new CheesePizza());}
                else
                {
                    Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                    continue;
                }
                Pizzas[^1].PrintPizza();    //displays the most recently created pizza
                if (!input.Contains("custom"))
                {
                    CustomOrNot(Pizzas[^1]);
                }
                i += NumberOf(Pizzas[^1]);    //This also adds those pizzas to the list
                PrintOrder();
                Console.WriteLine("\nAdd more pizzas? (Y/N)");
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
            Console.WriteLine($"\nYour {custom.Name} pizza is ready for order:");
            Pizzas[^1].PrintPizza();
            Console.WriteLine("\nWould you like to modify this pizza further? (Y/N)");
            if (CustomPizza.YesNo())
            {
                Pizzas.Remove(Pizzas[^1]);
                MakeCustomPizza(custom);
            }
        }
        public void CustomOrNot(APizzaModel p)
        {
            Console.WriteLine("\nWould you like to customize this pizza? (Y/N)");
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "y" | s == "yes")
            {
                MakeCustomPizza(p);
                Pizzas.Remove(p);    //remove the unmodified pizza from the order
            }
            else if (s != "n" & s != "no")
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                CustomOrNot(p);
            }
        }
        public int NumberOf(APizzaModel p)
        {
            Pizzas[^1].PrintPizza();
            Console.WriteLine("\nHow many of this pizza would you like to order? (Please type the number)");
            if (!int.TryParse(Console.ReadLine(), out int input) | input<0)
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                NumberOf(p);
            }
            if (input == 0)    //If the customer didn't mean to order that pizza
            {
                Pizzas.Remove(Pizzas[^1]);
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
            tprice = 0;
            Console.WriteLine("\nYour current order is:");
            foreach (APizzaModel p in Pizzas)
            {
                Console.WriteLine($"{p.Size} {p.Name} - {p.Price}");
                tprice += p.Price;
            }
            Console.WriteLine($"Total Price: ${tprice}");
        }

        public void Pay()
        {
            Console.WriteLine("\nPay for order now? (Y/N)");
            string s = Console.ReadLine().Trim().ToLower();
            if (s == "y" | s == "yes") {paid = true;}
            else if (s == "n" | s == "no") {paid = false;}
            else 
            {
                Console.WriteLine("\nSorry, we didn't catch that. Could you please repeat it?");
                Pay();
            }
        }
    }
}
