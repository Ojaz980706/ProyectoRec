using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTemplate.Models;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Collections.Generic;

namespace MVCTemplate.Controllers
{
    public class HomeController : Controller
    {
        

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


    }
}