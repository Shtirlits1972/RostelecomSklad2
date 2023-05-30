using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string DepartmentsName { get; set; }
        public string Deskr { get; set; }
        public override string ToString()
        {
            return DepartmentsName;
        }
    }
}


