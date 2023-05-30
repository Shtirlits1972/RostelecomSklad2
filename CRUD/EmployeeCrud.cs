using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RostelecomSklad2.Models;
using Dapper;

namespace RostelecomSklad2.CRUD
{
    public class EmployeeCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Employee>("SELECT Id, EmployeeName, PersonalData, Position, DepartmentsId, DepartmentsName FROM EmployeeView").ToList();
            }

            return list;
        }
        public static Employee GetOne(int Id)
        {
            Employee model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Employee>("SELECT Id, EmployeeName, PersonalData, Position, DepartmentsId, DepartmentsName FROM EmployeeView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Employee WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Employee model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Employee SET EmployeeName = @EmployeeName, PersonalData = @PersonalData, Position = @Position, DepartmentsId = @DepartmentsId WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Employee Add(Employee model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Employee (EmployeeName, PersonalData, Position, DepartmentsId) VALUES(@EmployeeName, @PersonalData, @Position, @DepartmentsId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
