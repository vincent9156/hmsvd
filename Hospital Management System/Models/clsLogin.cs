using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;

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
    public class checklogin: Conn
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

    public class forgotpass
    {
        public string Email { get; set;}
    }

    public class ckemail : Conn
    {
        public bool EmailCheck(forgotpass model)
        {
            bool Email = false;
            using (SqlCommand cmd = new SqlCommand("sp_LoginAddEditDelete", Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", "Email");
                cmd.Parameters.AddWithValue("@Username", model.Email);
               // cmd.Parameters.AddWithValue("@Password", model.Password);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        Email = true;
                    }
                    rdr.Close();
                }

            }
            return Email;
        }

    }
    public class MailTemplate
    {
        public string mailfrom { get; set; }
        public string mailto { get; set; }
        public string mailbody { get; set; }
        public string mailsubject { get; set; }
    }

    public class MailManager : Conn 
    {
        public void sendmail(MailTemplate mt)
        {
            MailAddress maFrom = new MailAddress(mt.mailfrom);
            MailMessage mm = new MailMessage();
            mm.From = maFrom;
            mm.To.Add(mt.mailto); //Multiple email address seprated by (",")
            mm.Subject = mt.mailsubject;
            mm.Body = mt.mailbody;
            mm.IsBodyHtml = true;
            SmtpClient sc = new SmtpClient();
            sc.Send(mm);
        }

        public MailTemplate readmailtemplate(string accountname)
        {
            MailTemplate mt = new MailTemplate();
            using (SqlCommand cmd = new SqlCommand("Sp_MailTemplateGet", Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@account", accountname);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        mt.mailbody = rdr["m_mailbody"].ToString();
                        mt.mailfrom = rdr["m_from"].ToString();
                        mt.mailsubject = rdr["m_subject"].ToString();
                    }
                    rdr.Close();
                }
            }
            return mt;
        }

    }
    public class loginerror : Conn
    {
        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }

}