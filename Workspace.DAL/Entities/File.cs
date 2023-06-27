using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.DAL.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Path { get; set; }

        #region Навигационные свойства

        public int UserId { get; set; }
        public User User { get; set; }

        public int FileTypeId { get; set; }
        public FileType FileType { get; set; }

        #endregion
    }
}
