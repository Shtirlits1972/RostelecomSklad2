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
    public class OrdersCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<string> OrdersStatusList = new List<string> { "черновик", "готов", "отгружен" };
        public static List<Orders> GetAll()
        {
            List<Orders> list = new List<Orders>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Orders>("SELECT Id, OrdersNumber, OrderDate, UsersId, UsersName, OrdersStatus FROM OrdersView").ToList();
            }
            return list;
        }
        public static Orders GetOne(int Id)
        {
            Orders model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Orders>("SELECT Id, OrdersNumber, OrderDate, UsersId, UsersName, OrdersStatus FROM OrdersView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                OrderDetailCrud.DeleteAll(Id);

                db.Execute("DELETE FROM Orders WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Orders model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Orders SET OrdersNumber = @OrdersNumber, OrderDate = @OrderDate, UsersId = @UsersId, OrdersStatus = @OrdersStatus  WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Orders Add(Orders model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Orders (OrdersNumber, OrderDate, UsersId, OrdersStatus) VALUES(@OrdersNumber, @OrderDate, @UsersId, @OrdersStatus ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
