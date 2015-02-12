using PizzaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDAL
{
    public class PizzaContext : DbContext
    {
        public static string ConnectionString = @"AKOSBOOK\SQLEXPRESS";
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderedPizza> OrderedPizzas { get; set; }
        public PizzaContext(string connStr) : base(connStr)
        {

        }
        public PizzaContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pizza>().
                HasMany(p => p.Toppings).
                WithMany().
                Map(
                 m =>
                     {
                         m.MapLeftKey("PizzaId");
                         m.MapRightKey("ToppingId");
                         m.ToTable("PizzaToppingMapping");
                     }
                );
            modelBuilder.Entity<Order>().
                HasMany(o => o.Pizzas).
                WithMany().
                Map(
                m =>
                {
                    m.MapLeftKey("OrderId");
                    m.MapRightKey("PizzaId");
                    m.ToTable("OrderPizzaMapping");
                });
        }
    }
}
