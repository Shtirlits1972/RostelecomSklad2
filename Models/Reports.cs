using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Reports
    {
        public int Id { get; set; } = 0;
        public string ReportsName { get; set; } = string.Empty;
        public string Deskr { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public override string ToString()
        {
            return $"Id = {Id}, ReportsName = {ReportsName}, Deskr = {Deskr}, CreationDate = {CreationDate}";
        }
    }
}


