using Evernote.Model;
using Evernote.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class NewNoteCommand(NotesVM notesVm) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM NotesVM { get; set; } = notesVm;

        public bool CanExecute(object parameter) => (parameter as Notebook) != null;

        public async void Execute(object parameter)
        {
            var selectedNotebook = parameter as Notebook;
            await NotesVM.CreateNoteAsync(selectedNotebook.Id);
        }
    }
}
