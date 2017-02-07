using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.Domain.Concrete
{
    public class Colour : IProductProperty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public int? ProductId { get; set; }

        public List<Product> Products { get; set; }
    }
}