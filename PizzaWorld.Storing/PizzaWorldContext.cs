using Microsoft.EntityFrameworkCore;
using PizzaWorld.Domain.Abstracts;
using PizzaWorld.Domain.Models;
using System.Collections.Generic;

namespace PizzaWorld.Storing
{
    public class PizzaWorldContext : DbContext
    {
        public DbSet<Store> Stores {get;set;}
        public DbSet<User> Users {get;set;}
        //Don't need to save Orders, Pizzas, or Toppings since their content already exists in 
        //the other data.

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=tcp:elliotpizzaworlddb.database.windows.net,1433;Initial Catalog=PizzaWorldDB;User ID=sqladmin;Password=Waffle-21;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Store>().HasKey(s => s.Id); 
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<APizzaModel>().HasKey(p => p.Id);
            builder.Entity<CheesePizza>();
            //You need at least one subclass because APizzaModel is abstract, but it doesn't need a key.
            builder.Entity<Order>().HasKey(o => o.Id);
            builder.Entity<Topping>().HasKey(t => t.Id);

            builder.Entity<Store>().Property(s => s.Id).ValueGeneratedNever();
            builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<Store>().HasData(new List<Store>
                {
                    new Store() {Id = 2, Name = "One"},
                    new Store() {Id = 3, Name = "Two"}
                }
            );
        }
    }
}
