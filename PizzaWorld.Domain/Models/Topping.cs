using PizzaWorld.Domain.Abstracts;

namespace PizzaWorld.Domain.Models
{
    public class Topping : AEntity
    {
        public string Name {get;set;}
        public Topping() {}
        //Apparently EntityFramework requires this.
        public Topping(string n)
        {
            Name = n;
        }
    }
}