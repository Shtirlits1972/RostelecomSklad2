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
    public class DeliveriesCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Deliveries> GetAll()
        {
            List<Deliveries> list = new List<Deliveries>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Deliveries>("SELECT Id, DeliveryDate, Qty, ProductId, ProductName, SupplayId, CompanyName FROM DeliveriesView").ToList();
            }
            return list;
        }
        public static Deliveries GetOne(int Id)
        {
            Deliveries model = null;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Deliveries>("SELECT Id, DeliveryDate, Qty, ProductId, ProductName, SupplayId, CompanyName FROM DeliveriesView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }
            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Deliveries WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Deliveries model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Deliveries SET DeliveryDate = @DeliveryDate, Qty = @Qty, ProductId = @ProductId, SupplayId = @SupplayId  WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Deliveries Add(Deliveries model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Deliveries (DeliveryDate, Qty, ProductId, SupplayId ) VALUES(@DeliveryDate, @Qty, @ProductId, @SupplayId ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
