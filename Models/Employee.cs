using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string PersonalData { get; set; }
        public string Position { get; set; }
        public int DepartmentsId { get; set; }
        public string DepartmentsName { get; set; }

        public override string ToString()
        {
            return EmployeeName;
        }

    }
}


