using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class LoginDetails
    {
        [Required(ErrorMessage = "Please Enter the User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter the Email Name")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        public string Role { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter the Password")]
        public string Password { get; set; }

    }
}
