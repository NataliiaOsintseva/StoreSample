using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Abstract
{
    public interface IProductProperty
    {
        int Id { get; set; }

        string Name { get; set; }

        int ? ProductId { get; set; }
    }
}
