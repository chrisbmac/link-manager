using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebAppProj3.Models
{
    public class DBPinnedLink
    {
        [Key]
        public int pinnedLinkID {get;set;}
        public int linkID {get;set;}
        public int categoryID {get;set;}

        [Required(ErrorMessage="Required: Please provide a link")]
        [MaxLength(100)]
        [Display(Name="Link")]
        [RegularExpression(@"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$", ErrorMessage="Problem: Check the URL you provided and ensure it is correct")]
        public string link {get; set;}
        
        [Required(ErrorMessage="Required: Please Provide a Category Name")]
        [MaxLength(25)]
        [Display(Name="Name")]
        [RegularExpression(@"^[a-zA-Z0-9\ ]*$", ErrorMessage="Problem: Please check *Name*, no special characters are allowed")]
        public string name {get; set;} 

    }
}