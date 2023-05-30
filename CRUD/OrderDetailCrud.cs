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
    public class OrderDetailCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<OrderDetail> GetAll(int OrderId)
        {
            List<OrderDetail> list = new List<OrderDetail>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<OrderDetail>(" SELECT Id, OrderId, ProductId, ProductName, Qty FROM OrderDetailView WHERE OrderId = @OrderId  ; ", new { OrderId }).ToList();
            }
            return list;
        }

        public static OrderDetail GetOne(int Id)
        {
            OrderDetail model = null;
            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<OrderDetail>("SELECT Id, OrderId, ProductId, ProductName, Qty FROM OrderDetailView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }
            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM OrderDetail WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(OrderDetail model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE OrderDetail SET OrderId = @OrderId, ProductId = @ProductId, Qty = @Qty WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static OrderDetail Add(OrderDetail model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO OrderDetail (OrderId, ProductId, Qty ) VALUES(@OrderId, @ProductId, @Qty ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
