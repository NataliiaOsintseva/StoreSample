using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Abstract
{
    public interface IOrder
    {
        void ProcessOrder(Cart cart, Shipment shipment);
    }
}
