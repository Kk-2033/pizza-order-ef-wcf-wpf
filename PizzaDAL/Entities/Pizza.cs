using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDAL.Entities
{
    public class Pizza
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public ICollection<Topping> Toppings { get; set; }
        public int Price {
            get
            {
                int price = 32 * 10;
                foreach (var item in Toppings)
                {
                    price += item.Price;
                }
                return price;
            }
            set
            {
                
            }
        }

        public int FamilyPrice
        {
            get
            {
                int price = Price * 2;
                foreach (var item in Toppings)
                {
                    price += item.Price;
                }
                return price;
            }
            set
            {

            }
        }

        public Pizza()
        {
            Toppings = new List<Topping>();
        }
    }
}
