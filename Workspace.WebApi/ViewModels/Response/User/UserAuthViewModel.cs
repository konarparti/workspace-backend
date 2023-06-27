namespace Workspace.WebApi.ViewModels.Response.User
{
    public class UserAuthViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public int UserRoleId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}
