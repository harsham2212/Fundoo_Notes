using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Note
{
    public class NoteUserResponse
    {
        public int userId { get; set; }
        public int noteId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
    }
}
