using System;

namespace PizzaWorld.Domain.Abstracts
{
    public abstract class AEntity
    {
        public long Id {get;set;}
        /*protected AEntity()
        {
            Id = DateTime.Now.Ticks;
            //This is only unique because, since we're making all orders through a single console,
            //it's impossible to make two orders in a single second.
        }*/
        //This would work if only stores and users are entities, but multiple pizzas or toppings can be
        //created at once, leading to non-unique IDs. We'll let SQL generate them for us,
        //guaranteeing uniqueness.
    }
}