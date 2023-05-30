using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQty { get; set; }

        public override string ToString()
        {
            return WarehouseName;
        }
    }
}


