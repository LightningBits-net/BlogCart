using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SharedServices.Data
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}

