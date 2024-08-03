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
        [DisplayName("User Id")] 
		public string? UserId { get; set; }

        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

		[DisplayName("Term and Condition")]
		public bool TNC { get; set; }

        [DisplayName("Password")]
        public string? Password { get; set; }

        [DisplayName("Register Date")]
        public DateTime? RegisterDate { get; set; }

        [DisplayName("Member Id")]
        public int? MemberId { get; set; }

        [DisplayName("Create Date")]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        [DisplayName("Member")]
        public string? Member => MemberId == null? "No" : MemberId.ToString();

        [NotMapped]
        [Display(Name = "Register Date")]
        public string RegisterDateFormatted
        {
            get
            {
                return RegisterDate?.ToString("dd/MM/yyyy");
            }
        }

    }
}
