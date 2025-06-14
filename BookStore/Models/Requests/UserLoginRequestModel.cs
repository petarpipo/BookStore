using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Requests
{
    public class UserLoginRequestModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
