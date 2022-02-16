using CommonLayer.Note;
using DocumentFormat.OpenXml.ExtendedProperties;
using RepositoryLayer.Entities;
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
        Task<List<Note>> GetAllNotes();
        public Task Color(int NoteId, string color);
        public Task ArchieveNote(int NoteId);
        public Task PinNote(int NoteId);
        public Task TrashNote(int NoteId);
        public IEnumerable<Note> GetAllNotesByNoteId(int NoteId);
    }
}
