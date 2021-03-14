using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace WebAppProj3.Models
{
    public class LinkManager : DbContext
    {
        // This class gets all data from tbl links and stores them in lists by category for easy display on views
        private DbSet<DBLink> tblLinks {get;set;}

        private bool _pinnedToTop;
        // list of all records in db ordered by name
        public List<DBLink> dblinks {
        get {
            List<DBLink> myList = tblLinks.OrderBy(l => l.name).ToList();
            return myList;
            }
        }

        public bool pinnedToTop {
            get {
                return _pinnedToTop;
            } set {
                _pinnedToTop = value;
            }
        }
        
        // get all of the categories and isolate them to use in making lists of each category to display on view
        public List<DBLink> categories {
            get {
                 List<DBLink> myList = (from cat in tblLinks select new DBLink{categoryID = cat.categoryID}).Distinct().ToList();
                return myList;
            }
        }
        // create list of category 0 of database table
        public List<DBLink> category_0 {
            get {   
                return dblinks.Where(t => t.categoryID == 1).Select(t => t).Distinct().ToList();
               
            }
        }

        // create list of category 1 of database table
        public List<DBLink> category_1 {
            get {
                return dblinks.Where(t => t.categoryID == 2).Select(t => t).Distinct().ToList();
            }
        }

        // create list of category 2 of database table
        public List<DBLink> category_2 {
            get {
                return dblinks.Where(t => t.categoryID == 3).Select(t => t).Distinct().ToList();
            }
        }

        // create list of category 3 of database table
         public List<DBLink> category_3 {
            get {    
                return dblinks.Where(t => t.categoryID == 4).Select(t => t).Distinct().ToList();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(Connection.CONNECTION_STRING);
        }
    }
}
