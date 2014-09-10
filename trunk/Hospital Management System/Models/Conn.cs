using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class Conn
    {
        private SqlConnection _Connection;
        public SqlConnection Connection
        {
            get
            {
                _Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                if (_Connection.State == ConnectionState.Closed)
                { _Connection.Open(); }
                return _Connection;
            }
        }

    }
}