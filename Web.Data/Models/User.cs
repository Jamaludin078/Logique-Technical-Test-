using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Models
{
	public class User
	{
		[Key]
		public string? UserId { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? Email { get; set; }

		public bool TNC { get; set; }

		public string? Password { get; set; }

		public DateTime? RegisterDate { get; set; }

		public int? MemberId { get; set; }

		public DateTime? CreateDate { get; set; }

	}
}
