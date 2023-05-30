using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public string UsersName { get; set; }
        public DateTime LoginDateTime { get; set; }
        public override string ToString()
        {
            return $" Id = {Id}, UsersId = {UsersId}, UsersName = {UsersName}, LoginDateTime = {LoginDateTime} ";
        }
    }
}


