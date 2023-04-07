using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoApiWeb.Models;

namespace ToDoApiWeb.Controllers
{
    public class ToDoController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                select Id,Name
                from dbo.ToDo
            ";
            DataTable dt = new DataTable();
            using(var con=new SqlConnection(ConfigurationManager.
                ConnectionStrings["ToDoDB"].ConnectionString))
                using(var cmd=new SqlCommand(query,con))
                using(var da=new SqlDataAdapter(cmd))
                {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
                }
            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }
        public string Post(ToDo toD)
        {
            try
            {
                string query = @"
                    insert into dbo.ToDo values
                    ('"+toD.Name+ @"')
                ";
                DataTable dt = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["ToDoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return "Save Successfully!";
            }
            catch (Exception)
            {
                return "Failed!";
            }
        }


        public string Put(ToDo toD)
        {
            try
            {
                string query = @"
                    update dbo.ToDo set Name=
                    '" + toD.Name + @"'
                    where Id="+toD.Id+@"
                ";
                DataTable dt = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["ToDoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return "Update Successfully!";
            }
            catch (Exception)
            {
                return "Failed!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                string query = @"
                    delete from  dbo.ToDo 
                    where Id=" + id + @"
                ";
                DataTable dt = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["ToDoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return "Delete Successfully!";
            }
            catch (Exception)
            {
                return "Failed!";
            }
        }
    }
}
