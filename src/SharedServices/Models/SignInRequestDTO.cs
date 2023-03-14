using System;
using System.ComponentModel.DataAnnotations;

namespace SharedServices.Models
{
	public class SignInRequestDTO
	{
        [Required(ErrorMessage = "UserEmail is required")]
        //[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        [EmailAddress(ErrorMessage ="Invalid UserEmail")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

