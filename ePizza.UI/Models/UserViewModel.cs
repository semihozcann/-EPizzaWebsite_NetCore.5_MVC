using System.ComponentModel.DataAnnotations;

namespace ePizza.UI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}
