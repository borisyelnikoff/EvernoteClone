using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class NewNoteCommand(NoteVM noteVm) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NoteVM NoteVm { get; set; } = noteVm;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
        }
    }
}
