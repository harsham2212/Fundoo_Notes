using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Note
{
    public class NotePostModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsReminder { get; set; }
        public string Color { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
    }
}
