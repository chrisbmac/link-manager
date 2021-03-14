using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAppProj3.Models
{
    public class CategoryManager : DbContext
    {   
        //--------------------This class is for getting and displaying category data from tblCategories to work with links  
        private DbSet<DBCategory> tblCategory {get;set;}

        public List<DBCategory> dbcategories {
            get {
                List<DBCategory> myList = tblCategory.ToList();
                return myList;
            }
        }
        // get the categpries data 
        public SelectList getCateg_SelectList() {
            List<DBCategory> listDB = tblCategory.OrderBy(cat => cat.categoryName).ToList();
            return new SelectList(listDB, "categoryID", "categoryName");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(Connection.CONNECTION_STRING);
        }

    }
}