using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Firstname is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "LastName is Required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "UserName is Required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "LastName is Required")]
		[MinLength(5, ErrorMessage = "Min Length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "This Field is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password Not Matching!")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }

	}
}
