using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class OrderDetail
    {
        public int Id { get; set; } = 0;
        public int OrderId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; } = 0;
        public override string ToString()
        {
            return $" Id = {Id}, OrderId = {OrderId}, ProductId = {ProductId}, ProductName = {ProductName}, Qty = {Qty} ";
        }
    }
}


