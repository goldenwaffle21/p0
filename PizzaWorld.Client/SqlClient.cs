using Microsoft.EntityFrameworkCore;
using PizzaWorld.Domain.Models;
using PizzaWorld.Storing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaWorld.Client
{
    public class SqlClient
    {
        private readonly PizzaWorldContext _db = new PizzaWorldContext();
        public SqlClient()
        {
            if (ReadStores().Count() == 0)
            {
                CreateStore();
            }
        }

        public IEnumerable<Store> ReadStores()
        {
            return _db.Stores;
        }
        public Store ReadOneStore(string name)
        {
            var s = _db.Stores.FirstOrDefault(s => s.name == name);
            return s;
        }

        /*public IEnumerable<Order> ReadOrders(Store store)
        {
            var s = ReadOneStore(store.Name);
            return s.Orders;
        }*/
        //Redundant except for double checking that the store is valid,
        //and that already occurs in SelectStore().

        public IEnumerable<User> ReadUsers()
        {
            return _db.Users;
        }
        public User ReadOneUser(long Id)
        {
            var u = _db.Users.FirstOrDefault(u => u.Id == Id);
            return u;
        }

        /*public IEnumerable<Order> ReadOrders(User user)
        {
            var u = ReadOneUser(user.Id);
            return u.Orders;
        }*/
        //Redundant except for double checking that the user is valid, and that
        //already occurs in SelectUser().

        public void Save(Store store)
        {
            _db.Add(store);
            _db.SaveChanges();
        }
        public void Save(User user)
        {
            _db.Add(user);
            _db.SaveChanges();
        }
        
        public void Update()
        {
            _db.SaveChanges();
        }
        public void CreateStore()
        {
            Save(new Store());
        }

        public Store SelectStore()
        {
            string input = Console.ReadLine();
            if (ReadOneStore(input)==null)    //input was invalid
            {
                Apologize();
                return SelectStore();
            }
            else {return ReadOneStore(input);}
        }

        public User SelectUser()
        {
            int.TryParse(Console.ReadLine(), out int input);
            var u = ReadOneUser(input);
            if (u==null)
            {
                Apologize();
                return SelectUser();
            }
            else {return u;}
        }

        public void Apologize()
        {
            Console.WriteLine("Sorry, we didn't catch that. Could you please repeat it?");
        }
    }
}
