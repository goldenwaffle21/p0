using PizzaWorld.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaWorld.Domain.Models
{
    public class User : AEntity
    {
        public List<Order> Orders {get;set;}
        public Store SelectedStore;
        public string Name {get;set;}
        public bool temporary = false;

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var p in Orders.Last().Pizzas)
            {
                sb.AppendLine(p.ToString());
            }
            return $"I have selected this store: {SelectedStore}";
        }

        public void PrintOrders()
        {
            foreach (Order o in Orders)
            {
                Console.WriteLine($"Order ID: {o.Id} ({o.status})");
            }
        }
        public void PrintUndeliveredOrders()
        {
            foreach (Order o in Orders)
            {
                if (o.status != "delivered")
                {
                    Console.WriteLine("Order ID: "+o.Id);
                }
            }
        }

        public Order SelectOrder()
        {
            long.TryParse(Console.ReadLine(),out long input);
            try
            {
                return Orders.Find(x => x.Id == input);
            }
            catch
            {
                Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
                return SelectOrder();
            }
        }
    }
}