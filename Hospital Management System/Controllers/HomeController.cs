using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.Controllers
{
    public class HomeController : Controller
    {
        ckemail Email = new ckemail();
        checklogin login = new checklogin();
        registerentry registernew = new registerentry();
        MailManager mm = new MailManager();
        MailTemplate mt = new MailTemplate();
        loginerror lerror = new loginerror();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public ActionResult register(hmsregister data)
        {
            MembershipCreateStatus sts;
            Membership.CreateUser(data.Username, data.Password, data.Username, data.question, data.answer, true, out sts);

            if (sts.ToString() == "Success")
            {

                if (!Roles.RoleExists("client"))
                {
                    Roles.CreateRole("client");
                }
                Roles.AddUserToRole(data.Username, "client");
                mt = mm.readmailtemplate("createuser");
                mt.mailto = data.Username;
                mm.sendmail(mt);
                ViewData["_error_"] = "New User registered successfully . Please Login to continue";
                return View();
            }
            else
            {
                ViewData["error"] = "Please Enter Password Minimum 5 Charecter";
                ViewData["_error_"] = lerror.GetErrorMessage(sts);
                return View();
            }

            if (registernew.userregister(data))
            {
                return Redirect("Login");
            }
            else
            {
                return View();

            }

            //return View();
        }
       [Authorize(Roles = "client")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
     //   [Authorize(Roles = "client")]
        public ActionResult Index(clsLogin data)
        {
            if (login.LoginCheck(data))
            {
                HttpCookie Cokkie = new HttpCookie("My Cookie");
                Cokkie.Expires = (DateTime.Now.AddMinutes(5));
                Cokkie["Username"] = data.Username;
                Cokkie["Password"] = data.Password;

                this.Session["Username"] = data.Username;
                this.Session["Password"] = data.Password;
                return RedirectToAction("Index","Entry");
                // return Register(data); 
            }
            else
            {
                ViewData["error"] = "Please Enter A Valid Username And Password";
                return View();
            }

        }

        public ActionResult Forgotpassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Forgotpassword(forgotpass data)
        {

            if (data.Email==null)
            {
                ViewData["error"] = "Please Enter the Email";
            }
            else if (Email.EmailCheck(data))
            {
                ViewData["Email"] = "hi";
                return View();
            }
            else
            {
                ViewData["error"] = "Plesse Enter the Register Email";
                return View();
            }
            

            return View();
        }
    }
      
}
