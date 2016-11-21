using Store.Domain.Entities;

namespace Store.Models
{
    public class CartListViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}