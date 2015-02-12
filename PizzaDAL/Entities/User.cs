using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
