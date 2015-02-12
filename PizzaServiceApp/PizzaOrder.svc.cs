using PizzaDAL;
using PizzaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PizzaServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PizzaOrder" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PizzaOrder.svc or PizzaOrder.svc.cs at the Solution Explorer and start debugging.
    public class PizzaOrder : IPizzaOrder
    {

        public bool Register(string userName, string password, string emailAddress)
        {
            bool registerSucceeded = false;
            using (PizzaContext ctx = new PizzaContext(PizzaContext.ConnectionString))
            {
                var q = from user in ctx.Users
                        where user.UserName == userName && user.Password == password
                        select user;

                if (q.ToList().Count == 0)
                {
                    ctx.Users.Add(new User
                    {
                        UserName = userName,
                        Password = password,
                        EmailAddress = emailAddress
                    });
                    ctx.SaveChanges();
                    registerSucceeded = true;
                }
            }
            return registerSucceeded;
        }
        public Guid Login(User user)
        {
            using (PizzaContext ctx = new PizzaContext(PizzaContext.ConnectionString))
            {
                var q = from u in ctx.Users
                        where u.UserName == user.UserName && user.Password == user.Password
                        select u;

                // the database contains the user-pass pair
                if (q.ToList().Count == 1)
                {
                    return SessionManager.Instance.AddUser(user.UserName);
                }
                // the database does not contain the user-pass pair
                else
                {
                    return new Guid();
                }
            }
        }
        public List<Pizza> GetAllPizzas(Guid guid)
        {
            if (SessionManager.Instance.ValidateUser(guid))
            {
                using (PizzaContext ctx = new PizzaContext(PizzaContext.ConnectionString))
                {
                    List<Pizza> pizzas = ctx.Pizzas.Include("Toppings").ToList();
                    return pizzas;
                }
            }
            else
            {
                return null;
            }
        }
        public List<Topping> GetAllToppings(Guid guid)
        {
            if (SessionManager.Instance.ValidateUser(guid))
            {
                using (PizzaContext ctx = new PizzaContext(PizzaContext.ConnectionString))
                {
                    List<Topping> toppings = ctx.Toppings.ToList();
                    return toppings;
                }
            }
            else
            {
                return null;
            }
        }
        public int GetPizzasPrice(Guid guid, Pizza pizza)
        {
            if (SessionManager.Instance.ValidateUser(guid))
            {
                return pizza.Price;
            }
            else
            {
                return -1;
            }
        }
        public bool ProceedOrder(Guid guid, List<OrderedPizza> pizzas)
        {
            if (SessionManager.Instance.ValidateUser(guid))
            {
                bool orderProceedSucceeded = false;
                using (PizzaContext ctx = new PizzaContext(PizzaContext.ConnectionString))
                {
                    string userName = SessionManager.Instance.GetUserNameByGuid(guid);

                    User user = (from u in ctx.Users
                                 where u.UserName == userName
                                 select u).FirstOrDefault();

                    ctx.Orders.Add(new Order
                    {
                        Pizzas = pizzas,
                        Status = Order.StatusCreated,
                        User = user
                    });
                    orderProceedSucceeded = true;
                    ctx.SaveChanges();
                }
                return orderProceedSucceeded;
            }
            else
            {
                return false;
            }
        }
        public bool Logout(Guid guid)
        {
            bool success = false;
            SessionManager.Instance.ActiveUsers.Remove(guid);
            success = true;
            return success;
        }
    }
}
