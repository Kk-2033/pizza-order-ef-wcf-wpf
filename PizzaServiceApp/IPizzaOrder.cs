using PizzaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PizzaServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPizzaOrder" in both code and config file together.
    [ServiceContract]
    public interface IPizzaOrder
    {
        [OperationContract]
        bool Register(string userName, string password, string emailAddress);
        [OperationContract]
        Guid Login(User user);
        [OperationContract]
        bool Logout(Guid guid);
        [OperationContract]
        List<Pizza> GetAllPizzas(Guid guid);
        [OperationContract]
        List<Topping> GetAllToppings(Guid guid);
        [OperationContract]
        int GetPizzasPrice(Guid guid, Pizza pizza);
        [OperationContract]
        bool ProceedOrder(Guid guid, List<OrderedPizza> pizzas);
    }
}
