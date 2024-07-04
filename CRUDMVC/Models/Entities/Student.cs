using System;
using System.ComponentModel.DataAnnotations;
namespace CRUDMVC.Models.Entities
{
    public class Student
    {
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		[StringLength(50, ErrorMessage = "Name must be at most 50 characters long.")]
		[RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name should only contain letters and spaces.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone is required.")]
		public string Phone { get; set; }

		public bool Subscribed { get; set; }
	}
}
