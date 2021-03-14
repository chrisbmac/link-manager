using System;
using Microsoft.AspNetCore.Mvc;
using WebAppProj3.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using WebAppProj3;

namespace WebAppProj3.Controllers
{
    public class AdminController : Controller
    {   
        // Admin controller
        private LinkManager linkManager;
        private CategoryManager catManager;
        private PinnedLinkManager pinnedLinkManager;
        public AdminController(LinkManager myLinkMan, CategoryManager myCatMan, PinnedLinkManager mypinman){
            linkManager = myLinkMan;
            catManager = myCatMan;
            pinnedLinkManager = mypinman;
        }
        
        // Main Admin page
        public IActionResult AdminIndex(){
            // If user hacked their way in
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            //To display different data from tbls
            ViewBag.adminIndexcategories = catManager.dbcategories;
                ViewBag.dbcategories = catManager.dbcategories;
                    ViewBag.dbPinCategories = pinnedLinkManager;
                        ViewBag.dbLinks = linkManager.dblinks;
            // Icons for buttons from custom script
            MyIcons myIcons = new MyIcons();
            ViewData["pencilIcon"] = myIcons.pencilIconD;
                ViewData["garbadeD"] = myIcons.garbageIcon_D;
                    ViewData["garbadeD2"] = myIcons.garbageIcon_D2;
                        ViewData["addIcon"] = myIcons.addIcon;

            return View(linkManager);
        }
        
        // Edit category by category ID
        [HttpGet]
        [Route("EditCategory/{catID}")]
        public IActionResult EditCategory(int catID){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            DBCategory mydbcat = new DBCategory();
            ViewBag.editcategories = catManager.dbcategories.Find(id => id.categoryID == catID);
            return View(mydbcat);
        }

        // Submit the editing of category by id and new cat name
        [HttpPost]
        public IActionResult SubmitEditCategory(DBCategory mydbcat,int catID,string categoryName) {
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            if (!ModelState.IsValid) return RedirectToAction("AdminIndex");
            try {
                DBCategory name = catManager.dbcategories.Find(id => id.categoryID == catID);
                    name.categoryName = categoryName;
                catManager.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine("Problem editing cat"  + e);
                TempData["adminFeedBack"] = "Unsuccessful edit of category";
            } finally {
                TempData["adminFeedBack"] = "Successful edit of category";
            }
            return RedirectToAction("AdminIndex");
        }

        //Adding a new link to the database
        [HttpGet]
        [Route("AddLink/{addcatid}")]
        public IActionResult AddLink(int addcatid) {
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            DBLink mylink = new DBLink();
            ViewBag.addLink_categoryID = catManager.dbcategories.Find(id => id.categoryID == addcatid);
                ViewBag.addLink_categoryName = catManager.dbcategories.Find(id => id.categoryID == addcatid);
            TempData["pinned"] = linkManager.pinnedToTop;
            return View(mylink);
        }
        // Adding a new link to database,
        // If user want it pinned add it to pinned db as well
        [HttpPost]
        public IActionResult SubmitAddLink(DBLink mydblink, string name, string link, int categoryID, bool pinnedToTop){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            if (!ModelState.IsValid) return RedirectToAction("AdminIndex");
            try {
                Console.WriteLine("from submit" + name + link + categoryID + pinnedToTop);
                linkManager.Add(mydblink);
                linkManager.SaveChanges();
                if(pinnedToTop == true){
                    DBPinnedLink dbpinlink = new DBPinnedLink();
                    dbpinlink.categoryID = categoryID;
                    dbpinlink.name = name;
                    dbpinlink.link = link;
                    pinnedLinkManager.Add(dbpinlink);
                    pinnedLinkManager.SaveChanges();
                }
            } catch (Exception e) {
                Console.WriteLine("Problem adding link"  + e);
                TempData["adminFeedBack"] = "Unsuccessful addition of link";
            } finally {
                TempData["adminFeedBack"] = "Successful addition of link" ;
            }
            return RedirectToAction("AdminIndex");
        }

        // edit a link by ID
        [HttpGet]
        [Route("EditLink/{linkID}")]
        public IActionResult EditLink(int linkID){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.selectListCatMan = catManager.getCateg_SelectList();
            TempData["linkID"] = linkID;
            ViewBag.link = linkManager.dblinks.Find(id => id.linkID == linkID);
            ViewBag.whichCat = catManager.dbcategories.Find(id => id.categoryID == ViewBag.link.categoryID); 
            return View();
        }
        // Submit the edit of link and whether or not they want it pinned, if yes add it to pinned tbl
        [HttpPost]
        public IActionResult SubmitEditLink(DBLink mydblink, string categoryID, int linkID, bool pinnedToTop, string name, string link){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            if (!ModelState.IsValid) return RedirectToAction("AdminIndex");
            try {
                int newcategoryID = Convert.ToInt32(categoryID);
                DBLink editedLink = linkManager.dblinks.Find(id => id.linkID == linkID);
                    editedLink.categoryID = newcategoryID;
                    editedLink.name = name;
                    editedLink.link = link;
                linkManager.SaveChanges();

                if(pinnedToTop == true){
                    DBPinnedLink dbpinlink = new DBPinnedLink();
                    dbpinlink.categoryID = newcategoryID;
                    dbpinlink.name = name;
                    dbpinlink.link = link;
                    pinnedLinkManager.Add(dbpinlink);
                    pinnedLinkManager.SaveChanges();
                }

             } catch (Exception e) {
                Console.WriteLine("Problem editing link"  + e);
                TempData["adminFeedBack"] = "Unsuccessful edit of link";
            } finally {
                TempData["adminFeedBack"] = "Successful edit of link";
            }
            return RedirectToAction("AdminIndex");
        }
        // Deletion of link
        [HttpGet]
        [Route("DeleteLink/{linkID}")]
        public IActionResult DeleteLink(int linkID){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            DBLink dblink = new DBLink();
            ViewBag.forDeleteLink = linkManager.dblinks.Find(id => id.linkID == linkID);
            return View(dblink);
        }
        // submit deletion of link
        public IActionResult SubmitDeleteLink(DBLink dBLink){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            try {
                linkManager.Remove(dBLink);
                linkManager.SaveChanges();
             } catch (Exception e) {
                Console.WriteLine("Problem deleting pinned link"  + e);
                TempData["adminFeedBack"] = "Unsuccessful deletion of link";
            } finally {
                TempData["adminFeedBack"] = "Successful deletion of link";
            }
            return RedirectToAction("AdminIndex");

        }

        //---------------------------------------------------- For Pinned Links Editing and Deleting
        // Edit pinned link
        [HttpGet]
        [Route("EditPinnedLink/{pinLinkID}")]
        public IActionResult EditPinnedLink(int pinLinkID){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            Console.WriteLine(pinLinkID);
            ViewBag.selectListCatMan = catManager.getCateg_SelectList();
            ViewBag.link = pinnedLinkManager.dbPinnedLinks.Find(id => id.pinnedLinkID == pinLinkID);
            TempData["linkID"] = pinLinkID;
            return View();
        }
        // Submit edit of pinned link
        [HttpPost]
        public IActionResult SubmitEditPinnedLink(DBPinnedLink mydbPinnedlink, string categoryID, int pinnedLinkID, string name, string link){
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            if (!ModelState.IsValid) return RedirectToAction("AdminIndex");
            try {
                int newcategoryID = Convert.ToInt32(categoryID);
                DBPinnedLink editedLink = pinnedLinkManager.dbPinnedLinks.Find(id => id.pinnedLinkID == pinnedLinkID);
                    editedLink.categoryID = newcategoryID;
                    editedLink.name = name;
                    editedLink.link = link;
                pinnedLinkManager.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine("Problem submitting pinned link"  + e);
                TempData["adminFeedBack"] = "Unsuccessful edit of pinned link";
            } finally {
                TempData["adminFeedBack"] = "Successful edit of pinned link";
            }
            return RedirectToAction("AdminIndex");
        }
        // Deletion of pinned link
        [HttpGet]
        [Route("DeletePinnedLink/{pinLinkID}")]
        public IActionResult DeletePinnedLink(int pinLinkID) {
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            DBPinnedLink dbPinLink = new DBPinnedLink();
            ViewBag.forDeleteLink = pinnedLinkManager.dbPinnedLinks.Find(id => id.pinnedLinkID == pinLinkID);
            
            return View(dbPinLink);
        }
        // Submit delete request for pinned link
        public IActionResult SubmitDeletePinnedLink(DBPinnedLink dbPinLink) {
            if (HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Login", "Login");
            }
            try {
                pinnedLinkManager.Remove(dbPinLink);
                pinnedLinkManager.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine("Problem deleting pinned link"  + e);
                TempData["adminFeedBack"] = "Unsuccessful deletion of pinned link";
            } finally {
                TempData["adminFeedBack"] = "Successful deletion of pinned link";
            }
            return RedirectToAction("AdminIndex");
        }
    }
}

