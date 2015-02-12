using GalaSoft.MvvmLight.Messaging;
using PizzaClient2.PizzaOrder;
using PizzaClient2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaClient2
{
    public class ViewModelMessage : MessageBase
    {
        public static string Message_Navigate = "Navigate";
        public static string Message_LoadPizzaList = "LoadPizzaList";
        public static string Message_LoadOrderSummary = "LoadOrderSummary";
        public static string Message_AddToCart = "AddtoCart";
        public static string Navigation_PizzaSelector = "PizzaSelector";
        public static string Navigation_ProceedOrder = "ProceedOrder";
        public static string Message_RemoveFromCart = "RemoveFromCart";
        public static string Message_OpenDialog = "OpenDialog";
        public static string Dialog_SelectQuantityAndSize = "SelectQuantityAndSize";
        public static string Dialog_OrderSuccess = "OrderSuccess";
        public static string Dialog_OrderFailed = "OrderFailed";
        public static string Message_ClearCart = "ClearCart";
        public static string Navigation_Login = "Login";
        public static string Dialog_LoginFailed = "LoginFailed";
        public static string Dialog_RegistrationFailed = "RegistrationFailed";
        public static string Dialog_RegistrationSuccess = "RegistrationSuccess";
        public Guid Guid { get; set; }
        public string Message { get; set; }
        public List<OrderedPizza> Orders { get; set; }
        public Pizza Pizza { get; set; }
        public List<OrderedPizza> OrderPart { get; set; }
        public string NavigateTo { get; set; }
        public string Dialog { get; set; }
        public OrderedPizza OrderedPizza { get; set; }

    }
}
