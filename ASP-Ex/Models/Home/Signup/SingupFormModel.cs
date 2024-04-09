﻿using Microsoft.AspNetCore.Mvc;

namespace ASP_Ex.Models.Home.Signup
{
	public class SingupFormModel
	{
		[FromForm(Name = "user-name")]
		public String UserName { get; set; } = null!;


		[FromForm(Name = "user-email")]
		public String UserEmail { get; set; } = null!;


		[FromForm(Name = "user-password")]
		public String Password { get; set; } = null!;


		[FromForm(Name = "user-repeat")]
		public String UserRepeat { get; set; } = null!;


		[FromForm(Name = "user-agreement")]
		public bool Agreement { get; set; }


		[FromForm(Name = "signup-button")]
		public bool HasData { get; set; } = false;

	}
}
