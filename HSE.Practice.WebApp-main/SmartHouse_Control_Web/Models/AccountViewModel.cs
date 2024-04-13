using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

namespace SmartHouse_Control_Web.Models
{
    public class LoginViewModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Login { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

    public class NewUser
    {
        public int RoomId { get; set; }

        [Display(Name = "Имя")]
        [Required]
        [RegularExpression(@"[\D]{1,}", ErrorMessage = "Имя некорректно")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"[\D]{1,}", ErrorMessage = "Фамилия некорректна")]
        public string SecondName { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}