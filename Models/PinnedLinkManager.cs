using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace WebAppProj3.Models
{
    public class PinnedLinkManager : DbContext
    {
        // This Class holds the pinned links data.
        // It will then store each pinned link into list by categories for easy display on view
        private DbSet<DBPinnedLink> tblPinnedLinks {get;set;}
        public List<DBPinnedLink> dbPinnedLinks {
            get {
                List<DBPinnedLink> myList = tblPinnedLinks.OrderBy(l => l.name).ToList();
                return myList;
            }
        }
        
        public List<DBPinnedLink> pinned_Categories {
            get {
                 List<DBPinnedLink> myList = (from cat in tblPinnedLinks select new DBPinnedLink{categoryID = cat.categoryID}).Distinct().ToList();
                return myList;
            }
        }

        public List<DBPinnedLink> pinned_Category_0 {
            get {
                return dbPinnedLinks.Where(t => t.categoryID == 1).Select(t => t).Distinct().ToList(); 
            }
        }

        public List<DBPinnedLink> pinned_Category_1 {
            get {    
                return dbPinnedLinks.Where(t => t.categoryID == 2).Select(t => t).Distinct().ToList();
            }
        }

        public List<DBPinnedLink> pinned_Category_2 {
            get {    
                return dbPinnedLinks.Where(t => t.categoryID == 3).Select(t => t).Distinct().ToList();
            }
        }

        public List<DBPinnedLink> pinned_Category_3 {
            get {   
                return dbPinnedLinks.Where(t => t.categoryID == 4).Select(t => t).Distinct().ToList();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(Connection.CONNECTION_STRING);
        }
    }
}