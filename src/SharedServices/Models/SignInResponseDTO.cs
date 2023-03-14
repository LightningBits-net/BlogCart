using System;
namespace SharedServices.Models
{
	public class SignInResponseDTO
	{
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}

