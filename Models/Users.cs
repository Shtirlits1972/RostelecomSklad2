using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UsersName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public override string ToString()
        {
            return UsersName;
        }
    }
}


