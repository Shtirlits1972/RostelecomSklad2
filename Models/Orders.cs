using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string OrdersNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int UsersId { get; set; }
        public string UsersName { get; set; }
        public string OrdersStatus { get; set; }

        public override string ToString()
        {
            return OrdersNumber;
        }
    }
}


