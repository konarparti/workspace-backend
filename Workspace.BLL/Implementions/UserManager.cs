using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using Workspace.BLL.DTO.User;
using Workspace.BLL.Interfaces;
using Workspace.Core.Enums;
using Workspace.DAL;

namespace Workspace.BLL.Implementions
{
    public class UserManager : IUserManager
    {
        private readonly DataContext _dataContext;

        public UserManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<UserDTO?> AuthorizeUser(UserAuthDTO userAuthDto)
        {
            var user = await _dataContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == userAuthDto.Login);

            if (user == null)
            {
                return null;
            }

            if (user.Status == (int)UserStatusCode.Blocked)
            {
                throw new AuthenticationException($"Пользователь {user.Login} заблокирован");
            }

            if (!BCrypt.Net.BCrypt.Verify(userAuthDto.Password, user.Password))
            {
                return null;
            }

            UserDTO userDto = new()
            {
                Id = user.Id,
                Login = user.Login,
                UserRoleId = user.UserRoleId,
                Created = user.Created,
            };

            return userDto;
        }
    }
}
