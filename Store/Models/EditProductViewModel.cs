﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Store.Infrastructure.Abstract;
using Store.Domain.Entities;
using System.Web.Mvc;

namespace Store.Models
{
    public class EditProductViewModel
    {
        [Required(ErrorMessage = "Insert product name, please")]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Insert product description, please")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price cannot be less than 0.01 EUR")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Specify category, please")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Specify colour, please")]
        [Display(Name = "Colour")]
        public string [] ProductColour { get; set; }

        [Display(Name = "Image")]
        public byte[] ImageData { get; set; }

    }

}