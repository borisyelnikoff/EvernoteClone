using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class NewNotebookCommand(NotesVM notesVM) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM NotesVM { get; set; } = notesVM;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
        }
    }
}
