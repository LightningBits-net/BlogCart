using System;
using SharedServices.Data;

namespace SharedServices.ViewModels
{
    public class Order
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}

