using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.BLL.DTO.User;

namespace Workspace.BLL.Interfaces
{
    public interface IUserManager
    {
        Task<UserDTO?> AuthorizeUser(UserAuthDTO userAuthDto);
    }
}
