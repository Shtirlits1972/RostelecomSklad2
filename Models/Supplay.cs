using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Supplay
    {
        public int Id { get; set; } = 0;
        public string CompanyName { get; set; } = "";
        public string ContactDetails { get; set; } = "";
        public string Location { get; set; } = "";

        public override string ToString()
        {
            return CompanyName;
        }
    }
}


