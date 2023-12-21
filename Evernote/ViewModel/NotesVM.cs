using Evernote.Model;
using Evernote.Persistence;
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
    public class NotesVM : INotifyPropertyChanged
    {
		private Notebook _selectedNotebook;

        public NotesVM()
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

        public async Task CreateNotebook()
        {
            using var context = new EvernoteDbContext();
            context.Notebooks.Add(new Notebook()
            {
                Name = "New notebook"
            });
            await context.SaveChangesAsync();
        }

        public async Task CreateNoteAsync(int notebookId)
        {
            using var context = new EvernoteDbContext();
            context.Notes.Add(new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New note"
            });
            await context.SaveChangesAsync();
        }
    }
}
