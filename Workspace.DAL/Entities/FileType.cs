using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.DAL.Entities
{
    public class FileType : BaseEntity
    {
        public string Name { get; set; }

        #region Навигационные свойства

        public IEnumerable<File> FIles { get; set; }

        #endregion
    }
}
