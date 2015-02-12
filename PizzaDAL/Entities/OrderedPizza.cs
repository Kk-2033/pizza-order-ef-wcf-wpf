using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDAL.Entities
{
    public class OrderedPizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Diameter { get; set; }
        public int Price { get; set; }
    }
}
