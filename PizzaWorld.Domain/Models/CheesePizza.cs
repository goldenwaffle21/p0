using PizzaWorld.Domain.Abstracts;
using System.Collections.Generic;

namespace PizzaWorld.Domain.Models
{
    public class CheesePizza : APizzaModel
    {
        protected override void AddName()
        {
            Name = "Cheese";
        }
        protected override void AddCrust()
        {
            Crust = "regular";
        }
        protected override void AddSize()
        {
            Size = "small";
        }
        protected override void AddSauce()
        {
            Sauce = "tomato";
        }
        protected override void AddToppings()
        {
            List<string> toppingnames = new List<string>{"cheese"};
            foreach (string s in toppingnames)
            {
                Toppings.Add(new Topping(s));
            }
        }
    }
}