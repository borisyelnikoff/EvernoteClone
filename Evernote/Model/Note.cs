using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evernote.Model
{
    public class Note
    {
        public int Id { get; set; }
        public int NotebookId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }

        public Notebook Notebook { get; set; }
    }
}
