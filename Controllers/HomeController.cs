using System.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppProj3.Models;

namespace WebAppProj3.Controllers
{
    public class HomeController : Controller
    {   
        // The home controller for only display links view
        private LinkManager linkManager;
        private CategoryManager catManager;
        private PinnedLinkManager pinnedLinkManager;
        public HomeController(LinkManager myLinkMan, CategoryManager myCatMan, PinnedLinkManager myPinMan)
        {
         linkManager = myLinkMan;
         catManager = myCatMan;
         pinnedLinkManager = myPinMan;
        }

        public IActionResult Index(){
            LinksLogin linkLogin = new LinksLogin(Connection.CONNECTION_STRING, HttpContext);
            linkLogin.Signout();
            ViewBag.dbcategories = catManager.dbcategories;
            ViewBag.dbPinCategories = pinnedLinkManager;
            return View(linkManager);
        }

    }
}
