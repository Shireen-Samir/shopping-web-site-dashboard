using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace task.Models
{
    public class Category
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryid { set; get; }

        [Required]
        [Display(Name = "Name")]
        public string categoryname { set; get; }

        [Display(Name = "Description")]
        public string categorydescription { set; get; }
        
        [Display(Name ="Picture")]
        public string categorypicture { set; get; }

        public virtual List<Product> Products { get; set; }
    }
}