using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.UserVm
{
    public class ApplicationUserVm
    {
        [Required]
        [DisplayName("Логин")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}