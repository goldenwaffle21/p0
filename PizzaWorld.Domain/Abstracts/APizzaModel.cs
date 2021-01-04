using System.Collections.Generic;

namespace PizzaWorld.Domain.Abstracts
{
    public abstract class APizzaModel : AEntity
    {
        public string Name {get;set;}
        public string Crust {get;set;}
        public string Size {get;set;}
        public string Sauce {get;set;}
        public List<string> Toppings {get;set;}
        public double Price {get;set;}

        protected APizzaModel()
        {
            AddName();
            AddCrust();
            AddSize();
            AddSauce();
            AddToppings();
            CalculatePrice();
        }
        protected virtual void AddName() {}
        protected virtual void AddCrust() {}
        protected virtual void AddSize() {}
        protected virtual void AddSauce() {}
        protected virtual void AddToppings() {}
        protected void CalculatePrice(){
        //The price doesn't need to be virtual, as the formula is the same regardless of pizza, so it
        //doesn't need to be overridden.
            if (Size == "small")
            {
                Price = 5;
            }
            else if (Size == "medium")
            {
                Price = 10;
            }
            else     //for "large", but will also catch custom sizes if they for some reason exist
            {
                Price = 15;
            }
            Price += Toppings.Count * 0.5;
        }
        public string ToppingsString()
        {
            string ts = "";
            foreach (var topping in Toppings)
            {
                ts += topping+", ";
            }
            return ts.Substring(-3);
        }
    }
}
