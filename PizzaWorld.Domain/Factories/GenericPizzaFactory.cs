using PizzaWorld.Domain.Abstracts;

namespace PizzaWorld.Domain.Factories
{
    public class GenericPizzaFactory
    {
        public T Make<T>() where T : APizzaModel, new()
        {
            return new T();
        }

        
    }
}