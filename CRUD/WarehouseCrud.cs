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
    public class WarehouseCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Warehouse> GetAll()
        {
            List<Warehouse> list = new List<Warehouse>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Warehouse>("SELECT Id, WarehouseName, ProductId, ProductName, ProductQty FROM WarehouseView").ToList();
            }
            return list;
        }
        public static Warehouse GetOne(int Id)
        {
            Warehouse model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Warehouse>("SELECT Id, WarehouseName, ProductId, ProductName, ProductQty FROM WarehouseView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Warehouse WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Warehouse model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Warehouse SET WarehouseName = @WarehouseName, ProductId = @ProductId, ProductQty = @ProductQty WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Warehouse Add(Warehouse model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Warehouse (WarehouseName, ProductId, ProductQty ) VALUES(@WarehouseName, @ProductId, @ProductQty); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
