using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "To pole nie może pozostać puste")]
        [MinLength(5, ErrorMessage = "Wymagane minimum 5 znaków!")]
        public string Login { get; set; }
    }
}