using System;
using System.ComponentModel.DataAnnotations;

namespace SharedServices.Models
{
	public class ToDoItemDTO
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter ToDoItem details")]
        public string ToDo { get; set; }

        public bool Completed { get; set; }

        public DateTime DateCreated { get; set; }

        public string Comment { get; set; }
    }
}

