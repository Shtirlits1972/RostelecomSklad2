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
    public class LoginLogCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<LoginLog> GetAll()
        {
            List<LoginLog> list = new List<LoginLog>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<LoginLog>("SELECT Id, UsersId, UsersName, LoginDateTime FROM LoginLogView").ToList();
            }
            return list;
        }
        public static LoginLog GetOne(int Id)
        {
            LoginLog model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<LoginLog>("SELECT Id, UsersId, UsersName, LoginDateTime  FROM LoginLogView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM LoginLog WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(LoginLog model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE LoginLog SET UsersId = @UsersId, LoginDateTime = @LoginDateTime WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static LoginLog Add(LoginLog model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO LoginLog (UsersId, LoginDateTime  ) VALUES(@UsersId, @LoginDateTime); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
