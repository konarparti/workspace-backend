using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.DAL.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }

        #region Навигационные свойства

        public IEnumerable<User> Users { get; set; }

        #endregion
    }
}
