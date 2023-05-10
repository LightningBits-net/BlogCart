using System;
using System.ComponentModel.DataAnnotations;

namespace SharedServices.Data
{
	public class ToDoItem
	{
        [Key]
        public int Id { get; set; }

        public string ToDo { get; set; }

        public bool Completed { get; set; }

        public DateTime DateCreated { get; set; }

        public string Comment { get; set; }

        //public int ClientId { get; set; }

        ////[ForeignKey("BlogCategoryId")]
        //public Client Client { get; set; }
    }
}

