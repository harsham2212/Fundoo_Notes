﻿using CommonLayer.Note;
using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public Task AddNotes(NotePostModel notesModel, int userId);
        public NotePostModel UpdateNotes(NotePostModel notes, int NoteId);
        public bool DeleteNotes(int NoteId);
        public IEnumerable<Note> GetAllNotes();
    }
}
