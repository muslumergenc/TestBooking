using System.ComponentModel.DataAnnotations;

namespace SanProject.Web.Models
{
    public class LoginUser
    {
        [Required]
        public string Agency { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
