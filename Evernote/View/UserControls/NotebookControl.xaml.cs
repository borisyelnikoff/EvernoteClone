using Evernote.Model;
using Evernote.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Evernote.View.UserControls
{
    /// <summary>
    /// Interaction logic for DisplayNotebook.xaml
    /// </summary>
    public partial class NotebookControl : UserControl
    {
        public NotebookControl()
        {
            InitializeComponent();
        }

        public Notebook Notebook
        {
            get => (Notebook)GetValue(NotebookProperty);
            set => SetValue(NotebookProperty, value);
        }

        // Using a DependencyProperty as the backing store for Notebook. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookControl), new PropertyMetadata(null, SetValues));

        public RenameStartCommand RenameStartCommand { get; set; }

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotebookControl notebookControl)
            {
                notebookControl.DataContext = notebookControl.Notebook;
            }
        }
    }
}
