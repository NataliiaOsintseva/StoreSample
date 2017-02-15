using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.Domain.Entities
{
    public class Colour
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}