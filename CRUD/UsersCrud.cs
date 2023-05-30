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
    public class UsersCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Users> GetAll()
        {
            List<Users> list = new List<Users>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Users>("SELECT Id, UsersName, Login, Password, Role FROM Users").ToList();
            }
            return list;
        }
        public static Users GetOne(int Id)
        {
            Users model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Users>("SELECT Id, UsersName, Login, Password, Role FROM Users WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }
            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Users WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Users model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Users SET UsersName = @UsersName, Login = @Login, Password = @Password, Role = @Role WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Users Add(Users model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Users (UsersName, Login, Password, Role ) VALUES(@UsersName, @Login, @Password, @Role); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }

        public static string[] RoleArr = { "user", "admin" };
    }
}
