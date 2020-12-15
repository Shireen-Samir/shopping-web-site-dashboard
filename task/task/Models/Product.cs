using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace task.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productid { set; get; }

        [Required]
        [Display(Name = "Name")]
        public string productname { set; get; }

        [Display(Name = "Description")]
        public string  productdescription { get; set; }

        [Required]
        [Display(Name = "Price")]
        public int price { get; set; }

        [Display(Name = "Picture")]
        public string productpicture { set; get; }

        public int categoryid { get; set; }

        public virtual Category Category { get; set; }


    }
}