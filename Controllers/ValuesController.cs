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
using MySql.Data.MySqlClient;


namespace ToDoApiWeb.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        //public HttpResponseMessage Get()
        //{
        //    var retVal = new { key1 = "value1", key2 = "value2" };
        //    return Request.CreateResponse(HttpStatusCode.OK, retVal);
        //    //new string[] { "value1", "value2" };
        //}

        // GET api/values/5

        // GET api/values/5
        //public DataTable GetPassportNo(string personNo)
        //{
        //    string query = @"SELECT * FROM dbo.tblRegister WHERE PersonNo='" + personNo + "'";
        //    DataTable dt = new DataTable();
        //    using (var con = new SqlConnection(ConfigurationManager.
        //        ConnectionStrings["ToDoDB"].ConnectionString))
        //    using (var cmd = new SqlCommand(query, con))
        //    using (var da = new SqlDataAdapter(cmd))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        da.Fill(dt);
        //    }
        //    return dt;
        //}
        [Route("api/[PassportNo]")]
        public DataTable GetPassportNo(string passportNo)
        {
            //string query = @"SELECT * FROM dbo.tblRegister WHERE PersonNo='" + deoNo + "'";
            //string query = @"select tbl_citizen.Name,tbl_citizen.Name_In_Myanmar,DATE_FORMAT(tbl_citizen.Date_Of_Birth,'%Y-%m-%d') As Date_Of_Birth,tbl_citizen.NrcNo,tbl_citizen.Father_Name,tbl_appliedpp.DEO_No,tbl_appliedpp.Passport_No,DATE_FORMAT(tbl_appliedpp.Date_Of_Issue,'%Y-%m-%d') As Date_Of_Issue,DATE_FORMAT(tbl_appliedpp.Date_Of_Expiry,'%Y-%m-%d') As Date_Of_Expiry,tbl_appliedpp.application_id
            //from tbl_appliedpp inner join tbl_citizen on tbl_appliedpp.Citizen_No=tbl_citizen.Citizen_No where tbl_appliedpp.Passport_No='" + passportNo + "'";
            //DataTable dtprint = new DataTable();
            ////DataTable dtenroll = new DataTable();
            //using (var con = new MySqlConnection(ConfigurationManager.
            //    ConnectionStrings["printserver"].ConnectionString))
            //using (var cmd = new MySqlCommand(query, con))
            //{
            //    using (var da = new MySqlDataAdapter(cmd))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        da.Fill(dtprint);
            //    }
            //}
            //return dtprint;




            string query = @"select tbl_citizen.Name,tbl_citizen.Name_In_Myanmar,DATE_FORMAT(tbl_citizen.Date_Of_Birth,'%Y-%m-%d') As Date_Of_Birth,tbl_citizen.NrcNo,tbl_citizen.Father_Name,tbl_appliedpp.DEO_No,tbl_appliedpp.Passport_No,DATE_FORMAT(tbl_appliedpp.Date_Of_Issue,'%Y-%m-%d') As Date_Of_Issue,DATE_FORMAT(tbl_appliedpp.Date_Of_Expiry,'%Y-%m-%d') As Date_Of_Expiry,tbl_appliedpp.application_id
            from tbl_appliedpp inner join tbl_citizen on tbl_appliedpp.Citizen_No=tbl_citizen.Citizen_No where tbl_appliedpp.Passport_No=?passportNo";
            DataTable dtprint = new DataTable();
            //DataTable dtenroll = new DataTable();
            using (var con = new MySqlConnection(ConfigurationManager.
                ConnectionStrings["printserver"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("?passportNo", passportNo);
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dtprint);
                }
            }
            return dtprint;





            //String loginconnectionString = "server=localhost;User ID=root;Password=r00tpass;database=passbaseprint";
            //string con = "server=localhost;User ID=root;Password=r00tpass;database=passbaseprint;";
            //MySqlConnection conn = new MySqlConnection();
            //conn.ConnectionString = con;
            //conn.Open();
            ////var con = new MySqlConnection((ConfigurationManager.ConnectionStrings["printserver"].ConnectionString));
            //MySqlCommand cmd;
            //string query = @"select tbl_citizen.Name,tbl_citizen.Name_In_Myanmar,DATE_FORMAT(tbl_citizen.Date_Of_Birth,'%Y-%m-%d') As Date_Of_Birth,tbl_citizen.NrcNo,tbl_citizen.Father_Name,tbl_appliedpp.DEO_No,tbl_appliedpp.Passport_No,DATE_FORMAT(tbl_appliedpp.Date_Of_Issue,'%Y-%m-%d') As Date_Of_Issue,DATE_FORMAT(tbl_appliedpp.Date_Of_Expiry,'%Y-%m-%d') As Date_Of_Expiry,tbl_appliedpp.application_id
            //from tbl_appliedpp inner join tbl_citizen on tbl_appliedpp.Citizen_No=tbl_citizen.Citizen_No where tbl_appliedpp.Passport_No=?passportNo";
            //cmd = new MySqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("?passportNo", passportNo);
            //MySqlDataReader dr = cmd.ExecuteReader();
            //DataTable dtprint = new DataTable();
            //dtprint.Load(dr);
            ////DataSet ds = new DataSet();
            ////ds.Tables.Add(dtprint);
            //return dtprint;

        }
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        //public DataTable PostData(string passportNo)
        //{
        //    DataTable dtprint = new DataTable();
        //    return dtprint;
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
