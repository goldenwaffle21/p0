using System;
using System.Collections;

namespace PizzaWorld.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var cs = ClientSingleton.Instance;
            cs.MakeAStore();
        }
        static IEnumerable<Store> GetStores()
        {
            return new List<Store>();
        }
        static void PrintAllStores()
        {
            foreach(var store in GetAllStores())
            {
                System.Console.WriteLine(store);
            }
        }
    }
}
