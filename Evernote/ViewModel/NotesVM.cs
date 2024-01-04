﻿using Evernote.Model;
using Evernote.Persistence;
using Evernote.ViewModel.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Evernote.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
		private Notebook _selectedNotebook;

        private Note _selectedNote;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            DeleteNotebookCommand = new DeleteNotebookCommand(this);
            DeleteNoteCommand = new DeleteNoteCommand(this);
            Notebooks = [];
            Notes = [];
            Application.Current.Dispatcher.BeginInvoke(async () => await GetNotebooks());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<Note> SelectedNoteChanged;

        public ObservableCollection<Notebook> Notebooks { get; set; }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public DeleteNotebookCommand DeleteNotebookCommand { get; set; }

        public DeleteNoteCommand DeleteNoteCommand { get; set; }

        public Notebook SelectedNotebook
        {
            get => _selectedNotebook;
            set 
            { 
                _selectedNotebook = value; 
                OnPropertyChanged(nameof(SelectedNotebook));
                Application.Current.Dispatcher.BeginInvoke(async () => await GetNotes());
            }
        }

        public Note SelectedNote 
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                SelectedNoteChanged?.Invoke(this, SelectedNote);
            }
        }

        public async Task CreateNotebookAsync()
        {
            using var context = new EvernoteDbContext();
            context.Notebooks.Add(new Notebook()
            {
                Name = $"Notebook"
            });
            await context.SaveChangesAsync();
            await GetNotebooks();
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
            await GetNotes();
        }

        public async Task DeleteNotebookAsync(int notebookId)
        {
            using var context = new EvernoteDbContext();
            if (SelectedNotebook is null)
            {
                return;
            }

            var notes = context.Notes.Where(n => n.NotebookId == notebookId);
            context.Notes.RemoveRange(notes);
            context.Notebooks.Remove(SelectedNotebook);
            await context.SaveChangesAsync();
            Notes.Clear();
            await GetNotebooks();
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            if (SelectedNote is null)
            {
                return;
            }

            using var context = new EvernoteDbContext();
            context.Notes.Remove(SelectedNote);
            await context.SaveChangesAsync();
            await GetNotes();
        }

        public async Task UpdateNoteAsync()
        {
            if (SelectedNote is null)
            {
                return;
            }

            using var context = new EvernoteDbContext();
            var noteInDb = await context.Notes.SingleOrDefaultAsync(n => n.Id == SelectedNote.Id);
            noteInDb.FileLocation = SelectedNote.FileLocation;
            await context.SaveChangesAsync();
        }

        private async Task UpdateNoteFileAsync(string newContent)
        {
            SelectedNote.FileLocation = Path.Combine(Directory.GetCurrentDirectory(), $"{SelectedNote.Id}.rtf");
            await File.WriteAllTextAsync(SelectedNote.FileLocation, newContent);
        }

        private async Task GetNotebooks()
        {
            using var context = new EvernoteDbContext();
            var notebooks = await context.Notebooks.ToListAsync();
            Notebooks.Clear();
            foreach (var notebook in notebooks) 
            {
                Notebooks.Add(notebook);
            }
        }

        private async Task GetNotes()
        {
            if (SelectedNotebook is null)
            {
                return;
            }

            using var context = new EvernoteDbContext();
            var notes = await context.Notes
                .Where(n => n.NotebookId == SelectedNotebook.Id)
                .ToListAsync();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
