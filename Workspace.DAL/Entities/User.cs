using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.DAL.Entities
{
    public class User : BaseEntity
    {
        public string ProfileImage { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public int Status { get; set; }

        #region Навигационные свойства

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public IEnumerable<File> Files { get; set; }

        #endregion
    }
}
