using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class Shipment
    {
        [Required(ErrorMessage = "Insert your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insert your address")]
        [Display(Name = "Home address")]
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool Discount { get; set; }
    
    }
}   