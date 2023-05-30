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
    public class ReportsCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Reports> GetAll()
        {
            List<Reports> list = new List<Reports>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Reports>("SELECT Id, ReportsName, Deskr, CreationDate FROM Reports").ToList();
            }
            return list;
        }
        public static Reports GetOne(int Id)
        {
            Reports model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Reports>("SELECT Id,  ReportsName, Deskr, CreationDate FROM Reports WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Reports WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Reports model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Reports SET ReportsName = @ReportsName, Deskr = @Deskr, CreationDate = @CreationDate WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Reports Add(Reports model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Reports ( ReportsName, Deskr, CreationDate ) VALUES(@ReportsName, @Deskr, @CreationDate); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
