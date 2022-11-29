using System;
using SharedServices.Data;

namespace SharedServices.Models
{
	public class OrderDTO
    {
        public OrderHeaderDTO OrderHeaders { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public OrderHeaderDTO OrderHeader { get; set; }
    }
}

