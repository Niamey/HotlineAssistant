using System.ComponentModel.DataAnnotations;

namespace Vostok.Hotline.Core.Common.Swagger.Authorize.Razore.Models
{
	public class UserModel
	{
		[Required(ErrorMessage = "Invalid login")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Invalid password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string Role { get; set; }
	}
}