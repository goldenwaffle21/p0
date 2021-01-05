using PizzaWorld.Domain.Abstracts;
using System;
using System.Collections.Generic;

namespace PizzaWorld.Domain.Models
{
    public class Store : AEntity
    {
        public List<Order> Orders{get;set;}
        public string Name {get;set;}
        public Store()
        {
            Orders = new List<Order>();
        }
        public void CreateOrder()
        {
            Order o = new Order();
            Orders.Add(o);
        }

        public bool DeleteOrder(Order order,User u)
        {
            try
            {
                Orders.Remove(order);
                u.Orders.Remove(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Order ModifyOrder(Order o,User u)
        //Create a new order, replace the old one with it on both the customer and store lists, and
        //delete the 
        {
            o.PrintOrder();
            Order o2 = new Order();
            Orders[Orders.FindIndex(x => x == o)] = o2;
            u.Orders[u.Orders.FindIndex(x => x == o)] = o2;
            if (o2.tprice < o.tprice)
            {
                Console.WriteLine($"\nYour order now costs ${(o.tprice-o2.tprice)} less than before; you have been refunded the difference");
            }
            else if (o2.tprice == o.tprice)
            {
                Console.WriteLine("\nYour order's price didn't change. No refunds or charges have been issued.");
            }
            else
            {
                Console.WriteLine($"\nYour order now costs ${(o2.tprice-o.tprice)} more than before; the difference has been charged to your account.");
            }
            return o2;
        }
    }
}
