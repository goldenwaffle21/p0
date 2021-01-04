using PizzaWorld.Domain.Abstracts;
using System.Collections.Generic;

namespace PizzaWorld.Domain.Models
{
    public class PestoPizza : APizzaModel
    {
        protected override void AddName()
        {
            Name = "Pesto";
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
            Sauce = "pesto";
        }
        protected override void AddToppings()
        {
            Toppings = new List<string>{"artichoke hearts","cheese","feta","garlic","mushrooms"};
        }
    }
}