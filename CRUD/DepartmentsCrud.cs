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
    public class DepartmentsCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Departments> GetAll()
        {
            List<Departments> list = new List<Departments>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Departments>("SELECT Id, DepartmentsName, Deskr FROM Departments").ToList();
            }

            return list;
        }
        public static Departments GetOne(int Id)
        {
            Departments model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Departments>("SELECT Id, DepartmentsName, Deskr FROM Departments WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Departments WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Departments model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Departments SET DepartmentsName = @DepartmentsName, Deskr = @Deskr WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Departments Add(Departments model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Departments (DepartmentsName, Deskr ) VALUES(@DepartmentsName, @Deskr ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
