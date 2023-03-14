using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServices.Data
{
	public class OrderDetail
	{
        public int Id { get; set; }

        public int OrderHeaderId { get; set; }

        public int ProductId { get; set; }
      
        public virtual Product Product { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string Size { get; set; }

        public string ProductName { get; set; }
    }
}

