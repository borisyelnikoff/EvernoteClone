using Evernote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class RenameNotebookStartCommand(NotesVM notesVM) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM NotesVM { get; set; } = notesVM;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => NotesVM.RenameNotebookStart();
    }
}
