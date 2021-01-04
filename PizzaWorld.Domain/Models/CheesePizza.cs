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
            Toppings = new List<string>{"cheese"};
        }
    }
}