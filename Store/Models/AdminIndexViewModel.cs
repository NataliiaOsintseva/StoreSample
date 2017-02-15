using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;

namespace Store.Models
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Colour> Colours { get; set; }
    }
}