using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class clsLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class hmsregister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
    public class checklogin : Conn
    {
        public bool LoginCheck(clsLogin model)
        {
            bool login = false;
            using (SqlCommand cmd = new SqlCommand("sp_LoginAddEditDelete", Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "BYPKID");
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        login = true;
                    }
                    rdr.Close();
                }

            }
            return login;
        }

    }
    public class registerentry : Conn
    {
        public bool userregister(hmsregister model)
        {
            bool register = false;

            using (SqlCommand cmd = new SqlCommand("sp_LoginAddEditDelete", Connection))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "ADD");
                cmd.Parameters.AddWithValue("@username", model.Username);
                cmd.Parameters.AddWithValue("@password", model.Password);
                cmd.Parameters.AddWithValue("@hintQuestion", model.question);
                cmd.Parameters.AddWithValue("@hintAnswer", model.answer);
                cmd.ExecuteNonQuery();
                register = true;  
            }

            return register;
        }

    }
}