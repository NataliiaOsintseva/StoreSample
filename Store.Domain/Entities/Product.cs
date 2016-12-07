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

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        //[Required(ErrorMessage = "Specify product color, please")]
        //public Dictionary<ProductColors, bool> Color { get; set; }

        public byte[] ImageData { get; set; }
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