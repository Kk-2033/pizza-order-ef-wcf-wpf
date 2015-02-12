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
    public static class Extensions
    {
        public static void RemoveByNameAndSize(this ObservableCollection<OrderedPizzaViewModel> collection, string name, int size)
        {
            OrderedPizzaViewModel requested = collection.Where(pvm => pvm.OrderedPizza.Name == name && pvm.OrderedPizza.Diameter == size).FirstOrDefault();
            collection.Remove(requested);
        }

        public static void RemoveByNameAndSize
            (this List<OrderedPizza> list, string name, int size)
        {
            OrderedPizza requested = list.Where(op => op.Name == name && op.Diameter == size).FirstOrDefault();
            list.Remove(requested);
        }
    }
}
