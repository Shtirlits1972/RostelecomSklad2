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
    public class ProductCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Product> GetAll()
        {
            List<Product> list = new List<Product>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Product>("SELECT Id, ProductName, Price, Qty, Specifications FROM Product").ToList();
            }

            return list;
        }
        public static Product GetOne(int Id)
        {
            Product model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Product>("SELECT Id,  ProductName, Price, Qty, Specifications FROM Product WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Product WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Product model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, Qty = @Qty, Specifications = @Specifications  WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Product Add(Product model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Product ( ProductName, Price, Qty, Specifications ) VALUES(@ProductName, @Price, @Qty, @Specifications ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
