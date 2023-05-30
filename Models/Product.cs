using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Product
    {
        public int Id { get; set; } = 0;
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; } = 0;
        public int Qty { get; set; } = 0;
        public string Specifications { get; set; } = "";
        public override string ToString()
        {
            return ProductName;
        }
    }
}


