﻿using Evernote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class DeleteNoteCommand(NotesVM notesVM) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM NotesVM { get; set; } = notesVM;

        public bool CanExecute(object parameter) => (parameter as Note) != null;

        public async void Execute(object parameter)
        {
            var decision = MessageBox.Show("Are you sure You want to delete this note?", "Confirm delete action", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (decision == MessageBoxResult.Yes) 
            {
                var note = parameter as Note;
                await NotesVM.DeleteNoteAsync(note.Id);
            }
        }
    }
}
