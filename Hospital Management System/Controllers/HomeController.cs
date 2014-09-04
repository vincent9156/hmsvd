using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.Controllers
{
    public class HomeController : Controller
    {
        checklogin login = new checklogin();
        registerentry registernew = new registerentry();
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
        public ActionResult register(hmsregister data)
        {
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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
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
                return Redirect("Home/Login");
                // return Register(data); 
            }
            else
            {
                ViewData["error"] = "Sorry";
                return View();
            }

        }
    }
      
}
