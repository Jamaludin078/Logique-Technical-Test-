using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Models
{
	public class UserLogin
	{
		[MaxLength(150)]
		public string? Email { get; set; }

		[MaxLength(250)]
		public string? Password { get; set; }
	}
	public class LoginResponse
	{
		public bool Success { get; set; }

		public string? Message { get; set; }

		public Result Data { get; set; }

		public class Result
		{

			public string? FirstName { get; set; }

			public string? LastName { get; set; }
		}
	}
}
