using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int LabelId { get; set; }
        
        [Required]
        public string LabelName { get; set; }

        public virtual Note Notes { get; set; }
        public virtual UserModel User { get; set; }
        [ForeignKey("Note")]
        public int NoteId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
    }
}
