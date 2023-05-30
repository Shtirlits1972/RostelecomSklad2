using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RostelecomSklad2.Models;

namespace RostelecomSklad2.CRUD
{
    public class SupplayCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Supplay> GetAll()
        {
            List<Supplay> list = new List<Supplay>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Supplay>("SELECT Id, CompanyName, ContactDetails, Location FROM Supplay").ToList();
            }
            return list;
        }
        public static Supplay GetOne(int Id)
        {
            Supplay model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Supplay>("SELECT Id, CompanyName, ContactDetails, Location FROM Supplay WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Supplay WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Supplay model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Supplay SET  CompanyName = @CompanyName, ContactDetails = @ContactDetails, Location = @Location  WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Supplay Add(Supplay model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Supplay ( CompanyName, ContactDetails, Location ) VALUES(@CompanyName, @ContactDetails, @Location ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
