using PizzaDAL;
using PizzaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (PizzaContext ctx = new PizzaContext(PizzaContext.ConnectionString))
            {
                User testUser = new User
                {
                    UserName = "akos",
                    Password = "pass",
                    EmailAddress = "akos@akos.com"
                };

                Topping cheese = new Topping
                {
                    Name = "Cheese",
                    Price = 100
                };
                Topping tomatoSauce = new Topping
                {
                    Name = "Tomato Sauce",
                    Price = 50
                };
                Topping ham = new Topping
                {
                    Name = "Ham",
                    Price = 200
                };
                Topping mushroom = new Topping
                {
                    Name = "Mushroom",
                    Price = 100
                };
                Topping corn = new Topping
                {
                    Name = "Corn",
                    Price = 50
                };
                Topping salami = new Topping
                {
                    Name = "Salami",
                    Price = 150
                };
                Topping egg = new Topping
                {
                    Name = "Egg",
                    Price = 100
                };
                Topping onion = new Topping
                {
                    Name = "Onion",
                    Price = 50
                };
                Topping pineapple = new Topping
                {
                    Name = "Pineapple",
                    Price = 80
                };
                Topping beans = new Topping
                {
                    Name = "Beans",
                    Price = 80
                };
                Topping bacon = new Topping
                {
                    Name = "Bacon",
                    Price = 210
                };
                Topping tuna = new Topping
                {
                    Name = "Tuna",
                    Price = 180
                };
                Topping sourCream = new Topping
                {
                    Name = "Sour cream",
                    Price = 50
                };
                Topping chili = new Topping{
                    Name = "Chili",
                    Price = 40
                };

                Pizza margherita = new Pizza
                {
                    Name = "Margherita",
                    Toppings = new List<Topping>(){
                        tomatoSauce, cheese
                    }
                };
                Pizza salamiP = new Pizza
                {
                    Name = "Salami",
                    Toppings = new List<Topping>(){
                        tomatoSauce, salami, cheese
                    }
                };
                Pizza hawaii = new Pizza
                {
                    Name = "Hawaii",
                    Toppings = new List<Topping>(){
                        sourCream, ham, pineapple, cheese
                    }
                };
                Pizza songoku = new Pizza
                {
                    Name = "Son-go-ku",
                    Toppings = new List<Topping>()
                    {
                        cheese, tomatoSauce, ham, mushroom, corn
                    }
                };
                Pizza mexican = new Pizza
                {
                    Name = "Mexican",
                    Toppings = new List<Topping>(){
                        tomatoSauce, ham, beans, corn, sourCream, chili
                    }
                };

                ctx.Users.Add(testUser);

                ctx.Toppings.Add(cheese);
                ctx.Toppings.Add(tomatoSauce);
                ctx.Toppings.Add(ham);
                ctx.Toppings.Add(mushroom);
                ctx.Toppings.Add(corn);
                ctx.Toppings.Add(salami);
                ctx.Toppings.Add(egg);
                ctx.Toppings.Add(onion);
                ctx.Toppings.Add(pineapple);
                ctx.Toppings.Add(bacon);
                ctx.Toppings.Add(beans);
                ctx.Toppings.Add(tuna);
                ctx.Toppings.Add(sourCream);
                ctx.Toppings.Add(chili);

                ctx.Pizzas.Add(margherita);
                ctx.Pizzas.Add(salamiP);
                ctx.Pizzas.Add(hawaii);
                ctx.Pizzas.Add(songoku);
                ctx.Pizzas.Add(mexican);

                ctx.SaveChanges();
            }
        }
    }
}
