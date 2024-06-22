using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using Task2_noufal.Model;

namespace Task2_noufal.Controllers
{


    public class TestController : Controller
    {

        [HttpGet]
        [Route("Test/getStudent")]
        public JsonResult getStudent()
        {
            return Json(new { x = new { id = 1, name = "Noufal", age = 20 } });
        }


        [HttpGet]
        [Route("Test/getStuList")]
        public JsonResult getStuList()
        {

            List<Student> studentsList = [];
            List<Teacher> teacherList = new List<Teacher>();
            studentsList.Add(new Student { id = 1, name = "Noufal", age = 20 });
            studentsList.Add(new Student { id = 2, name = "ARun", age = 20 });
            studentsList.Add(new Student { id = 3, name = "Kumar", age = 20 });
            studentsList.Add(new Student { id = 4, name = "siva", age = 20 });

            teacherList.Add(new Teacher { id = 1, name = "abcd", dept = "cs" });

            teacherList.Add(new Teacher { id = 1, name = "abcd", dept = "cs" });
            return Json(new { school = new { studentsList, teacherList } });
        }


        [HttpGet]
        [Route("Test/getstuListFromDB")]
        public JsonResult getstuListFromDB(int id)
        {
            List<Student1> list3 = new List<Student1>();
            string conString = "Data Source=System8 ;Initial Catalog=Noufal; Integrated Security=True; Trust Server Certificate=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query = "select * from t2 where id in"+id ;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list3.Add(new Student1
                {
                    id = (int)reader["id"],
                    name = (string)reader["name"],
                    age = (int)reader["age"],
                    salary = (int)reader["salary"]

                });
            }
            con.Close();
            return Json(new { studentList = list3 });
        }






        [HttpGet]
        [Route("Test/getstuListusingParam")]
        public JsonResult getstuListusingParam(int id, string name)
        {
            List<Student1> list3 = new List<Student1>();
            string conString = "Data Source=System8 ;Initial Catalog=Noufal; Integrated Security=True; Trust Server Certificate=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query = "select * from t2 where id=" + id + "and name='" + name + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list3.Add(new Student1
                {
                    id = (int)reader["id"],
                    name = (string)reader["name"],
                    age = (int)reader["age"],
                    salary = (int)reader["salary"]

                });
            }
            con.Close();
            return Json(new { studentList = list3 });
        }


        [HttpGet]
        [Route("Test/getdatafromdatabase")]

        public JsonResult getdatafromdatabase(int customer_id,string customer_name)
        {

            List<student2> list4 = new List<student2>();
            string connectstring = "Data Source = System8; Initial Catalog = Nouf; Integrated Security = True;Trust Server Certificate = True";
            SqlConnection connect = new SqlConnection(connectstring);
            connect.Open();
            SqlCommand cmd = new SqlCommand("getData", connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customer_id", customer_id);
            cmd.Parameters.AddWithValue("@customer_nam", customer_name);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list4.Add(new student2
                {
                   customer_id = (int)reader["customer_id"],
                    customer_name = (string)reader["customer_name"],
                    address = (string)reader["address"],
                    city = (string)reader["city"],
                    state = (string)reader["state"],
                    zip_code = (int)reader["zip_code"]


                });
            }

            connect.Close();
            return Json(new {customerList= list4 });

        }

        [HttpPost]
        [Route("Test/insertData")]

        public JsonResult insertData([FromBody] student2 s)
        {
            string connectstring = "Data Source = System8; Initial Catalog = nouf; Integrated Security = True;Trust Server Certificate = True";
            SqlConnection connect = new SqlConnection(connectstring);
            connect.Open();
            SqlCommand cmd = new SqlCommand("postData", connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customer_id", s.customer_id);
            cmd.Parameters.AddWithValue("@customer_name", s.customer_name);
            cmd.Parameters.AddWithValue("@address", s.address);
            cmd.Parameters.AddWithValue("@city", s.city);
            cmd.Parameters.AddWithValue("@state", s.state);
            cmd.Parameters.AddWithValue("@zip_code", s.zip_code);
            string result = cmd.ExecuteScalar().ToString();
            if(result=="Success")
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new {success=false });
            }
          


        }

        [HttpPut]
        [Route("Test/insertData")]

        public JsonResult EditRecord([FromBody] student2 s)
        {
            string connectstring = "Data Source = System8; Initial Catalog = nouf; Integrated Security = True;Trust Server Certificate = True";
            SqlConnection connect = new SqlConnection(connectstring);
            connect.Open();
            SqlCommand cmd = new SqlCommand("editData", connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customer_id", s.customer_id);
            cmd.Parameters.AddWithValue("@customer_name", s.customer_name);
            cmd.Parameters.AddWithValue("@address", s.address);
            cmd.Parameters.AddWithValue("@city", s.city);
            cmd.Parameters.AddWithValue("@state", s.state);
            cmd.Parameters.AddWithValue("@zip_code", s.zip_code);
            string result = cmd.ExecuteScalar().ToString();
            if (result == "Success")
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }



        }
        [HttpDelete]
        [Route("Test/deleteRecord")]

        public JsonResult deleteRecord([FromBody] student2 s)
        {
            string connectstring = "Data Source = System8; Initial Catalog = nouf; Integrated Security = True;Trust Server Certificate = True";
            SqlConnection connect = new SqlConnection(connectstring);
            connect.Open();
            SqlCommand cmd = new SqlCommand("removeData", connect);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customer_id", s.customer_id);
            cmd.Parameters.AddWithValue("@customer_name", s.customer_name);
            cmd.Parameters.AddWithValue("@address", s.address);
            cmd.Parameters.AddWithValue("@city", s.city);
            cmd.Parameters.AddWithValue("@state", s.state);
            cmd.Parameters.AddWithValue("@zip_code", s.zip_code);
            string result = cmd.ExecuteScalar().ToString();
            if (result == "Success")
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }



        }
    }
}
