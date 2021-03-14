using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAppProj3.Models
{
    public class DBCategory
    {
       [Key]
       public int categoryID {get;set;}

        [Required(ErrorMessage="Required: Please Provide a Category Name")]
        [MaxLength(15)]
        [Display(Name="Category")]
        [RegularExpression(@"^[a-zA-Z0-9\ ]*$", ErrorMessage="Problem: Please check *Category Name*, no special characters are allowed")]
       public string categoryName{get;set;}         
    }
}