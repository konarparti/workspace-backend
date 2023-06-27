using System.ComponentModel.DataAnnotations;

namespace Workspace.WebApi.ViewModels.Requests.User
{
    public class UserAuthModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Логин должен быть указан.")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль должен быть указан.")]
        public string Password { get; set; }
    }
}
