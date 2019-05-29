using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Budget.Web.Mvc.Models.UserVm
{
    public class ApplicationUserVm
    {
        [Required]
        [DisplayName("Login")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}