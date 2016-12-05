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
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Insert product name, please")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Insert product description, please")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Price cannot have negative value")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Specify category, please")]
        public string Category { get; set; }

        //[Required(ErrorMessage = "Specify product color, please")]
        //public Dictionary<ProductColors, bool> Color { get; set; }

        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
    }

    public enum ProductColors
    {
        Black,
        Blue,
        Green,
        Pink,
        Red,
        Violate,
        White,
        Yellow
    }
}