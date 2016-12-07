using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Store.Infrastructure.Abstract
{
    public interface IProductModel
    {
        string Name { get; set; }
        string Description { get; set; }

        decimal Price { get; set; }

        string Category { get; set; }

        //Dictionary<ProductColors, bool> Color { get; set; }

        HttpPostedFileBase Image { get; set; }
        string ImageMimeType { get; set; }

        byte[] convertToByteArray(HttpPostedFileBase file);
    }
}
