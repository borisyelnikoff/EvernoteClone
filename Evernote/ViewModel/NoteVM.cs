using Evernote.Model;
using Evernote.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.ViewModel
{
    public class NoteVM : INotifyPropertyChanged
    {
		private Notebook _selectedNotebook;

        public NoteVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Notebook> Notebooks { get; set; }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public Notebook SelectedNotebook
        {
            get => _selectedNotebook;
            set 
            { 
                _selectedNotebook = value; 
            }
        }

    }
}
