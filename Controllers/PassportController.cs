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
    public class PassportController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("api/Passport")]
            
        public DataTable GetPassportNo(string passportNo)
        {
            DataTable dt3 = new DataTable();
            dt3.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Name_In_Myanmar"),
                new DataColumn("Date_Of_Birth"),
                new DataColumn("Nrc_No"),
                new DataColumn("DEO_No"),
                new DataColumn("Passport_No"),
                new DataColumn("Date_Of_Issue"),
                new DataColumn("Date_Of_Expiry"),
                 new DataColumn("application_id"),
                new DataColumn("Issuing_Office")
                });
            

            string query = @"select tbl_citizen.Name,tbl_citizen.Name_In_Myanmar,DATE_FORMAT(tbl_citizen.Date_Of_Birth,'%Y-%m-%d') As Date_Of_Birth,tbl_citizen.NrcNo As Nrc_No,tbl_citizen.Father_Name,tbl_appliedpp.DEO_No,tbl_appliedpp.Passport_No,DATE_FORMAT(tbl_appliedpp.Date_Of_Issue,'%Y-%m-%d') As Date_Of_Issue,DATE_FORMAT(tbl_appliedpp.Date_Of_Expiry,'%Y-%m-%d') As Date_Of_Expiry,'Yangon' As Issuing_Office
            from tbl_appliedpp inner join tbl_citizen on tbl_appliedpp.Citizen_No=tbl_citizen.Citizen_No 
            where tbl_appliedpp.IsDeleted=0 and tbl_appliedpp.Passport_No=?passportNo";

            DataTable dtprint = new DataTable();
            //MySqlConnection cnmycon1 = new MySqlConnection(ConfigurationManager.
            //ConnectionStrings["printserver"].ConnectionString);
            //MySqlCommand cmd1 = new MySqlCommand(query, cnmycon1);
            //cmd1.Parameters.AddWithValue("?passportNo", passportNo);
            //MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
            //cmd1.CommandType = CommandType.Text;
            //da1.Fill(dtprint);


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



            if (dtprint.Rows.Count > 0)
            {
                string enrollquery = @"select tblduration.application_id As Application_Id
                from tblduration where tblduration.deo_no= '" + dtprint.Rows[0]["Deo_No"].ToString().Trim() + "'";
                DataTable dtprint1 = new DataTable();

                using (var con = new MySqlConnection(ConfigurationManager.
                    ConnectionStrings["enrollserver"].ConnectionString))
                using (var cmd = new MySqlCommand(enrollquery, con))
                {
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(dtprint1);
                    }
                }


                dtprint.Merge(dtprint1, false, MissingSchemaAction.Add);
                var result = (from t in dtprint.AsEnumerable()
                              select new
                              {
                                  Name = t["Name"].ToString(),
                                  NameInMyanmar = t["Name_In_Myanmar"].ToString(),
                                  Dob = t["Date_Of_Birth"].ToString(),
                                  nrcno = t["Nrc_No"].ToString(),
                                  deono = t["DEO_No"].ToString(),
                                  passportno = t["Passport_No"].ToString(),
                                  issuedate = t["Date_Of_Issue"].ToString(),
                                  expiredate = t["Date_Of_Expiry"].ToString(),
                                  issuingoffice = t["Issuing_Office"].ToString(),
                                  applicationid = t["application_id"].ToString()
                              }).ToList();
                //foreach (var item in result)
                //{
                //    applicationid = item.applicationid;
                //}

                dt3.Rows.Add(result[0].Name.ToString(), result[0].NameInMyanmar.ToString(), result[0].Dob.ToString(), result[0].nrcno.ToString(), result[0].deono.ToString(), result[0].passportno.ToString(), result[0].issuedate.ToString(), result[0].expiredate.ToString(), result[1].applicationid.ToString(), result[1].applicationid.ToString());

                return dt3;
            }
            else
            {
                string query1 = @"select tbl_citizen.Name,tbl_citizen.Name_In_Myanmar,DATE_FORMAT(tbl_citizen.Date_Of_Birth,'%Y-%m-%d') As Date_Of_Birth,tbl_citizen.NrcNo As Nrc_No,tbl_citizen.Father_Name,tbl_appliedpp.DEO_No,tbl_appliedpp.Passport_No,DATE_FORMAT(tbl_appliedpp.Date_Of_Issue,'%Y-%m-%d') As Date_Of_Issue,DATE_FORMAT(tbl_appliedpp.Date_Of_Expiry,'%Y-%m-%d') As Date_Of_Expiry,'' As Application_Id,tbl_location.LocationName As Issuing_Office FROM tbl_appliedpp INNER JOIN tbl_citizen ON tbl_appliedpp.Citizen_No = tbl_citizen.Citizen_No INNER JOIN tbl_location ON tbl_appliedpp.IssuingOffice = tbl_location.LocationCode where tbl_appliedpp.Passport_No=?passportNo";
                DataTable dtprint1 = new DataTable();
                MySqlConnection cnmycon1 = new MySqlConnection(ConfigurationManager.
                ConnectionStrings["printserver"].ConnectionString);

                using (var con = new MySqlConnection(ConfigurationManager.
                    ConnectionStrings["otherserver"].ConnectionString))
                using (var cmd = new MySqlCommand(query1, con))
                {
                    cmd.Parameters.AddWithValue("?passportNo", passportNo);
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(dtprint1);
                    }
                }
                return dtprint1;
            }

        }
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}