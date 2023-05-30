using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Deliveries
    {
        public int Id { get; set; } = 0;
        public DateTime DeliveryDate { get; set; }
        public int Qty { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int SupplayId { get; set; } = 0;
        public string CompanyName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Id = {Id}, DeliveryDate = {DeliveryDate}, Qty = {Qty}, ProductId = {ProductId}, ProductName = {ProductName}, SupplayId = {SupplayId}, CompanyName = {CompanyName} ";
        }
    }
}
