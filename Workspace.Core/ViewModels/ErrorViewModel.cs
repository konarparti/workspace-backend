using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Core.ViewModels
{
    /// <summary>
    /// Модель ошибки.
    /// </summary>
    /// <param name="Message">Сообщение об ошибке.</param>
    public record ErrorViewModel(string Message);
}
