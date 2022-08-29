using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTemplate.Models;
using System.Globalization;
using System.Web.Security;
using MVCTemplate.Class;

namespace ComcellGeneratorCVI.Controllers
{
    public class LoginController : Controller
    {
        WindowsUser ActiveDirectory = new WindowsUser();
        /// <summary>
        /// Open Login view
        /// </summary>
        /// <returns>login view and error message in view bag</returns>
        // GET: Login
        public ActionResult Index(string message = "")
        {
            //Return Error Message
            ViewBag.errorMessage = message;
            return View();
        }
        /// <summary>
        /// Login logic
        /// </summary>
        /// <param name="username">windows user</param>
        /// <param name="password">windows password</param>
        /// <returns>Home index view</returns>
        //POST: Login/Signin
        [HttpPost]
        public ActionResult Signin(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                //Windows user model
                WindowsUserViewModel AD_User = ActiveDirectory.FindUser(username, password);
                //App user model
                //Validate if user excists
                if (AD_User != null)
                {
                    //Return view with user authentication 
                    FormsAuthentication.SetAuthCookie(AD_User.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", new { message = "user not found" });
                }
            }
            else
            {
                //return message for empty user
                return RedirectToAction("Index", new { message = "Insert username and/or password" });
            }

        }
        /// <summary>
        /// logout logic
        /// </summary>
        /// <returns>login view</returns>
        //POST: Login/Logout
        [Authorize]
        public ActionResult Logout()
        {
            //Finish session 
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}