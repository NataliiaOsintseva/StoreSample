using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price cannot be less than 0.01 EUR")]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Display(Name = "Image")]
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        
        [Display(Name = "Colour")]
        public IDictionary<string, bool> ProductColour { get; set; }
    }

    //public enum Category
    //{
    //    Bedroom,
    //    LivingRoom,
    //    Kitchen,
    //    Bathroom,
    //    Other
    //}

    //public enum Colour
    //{
    //    White,
    //    Pink,
    //    Purple,
    //    Red,
    //    Green,
    //    Blue,
    //    Black,
    //    Yellow,
    //    Other
    //}

}