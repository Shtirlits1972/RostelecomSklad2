using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostelecomSklad2
{
    public class Ut
    {
        public static readonly string strConn = ConfigurationManager.AppSettings["SqlConnString"];
    }
}
