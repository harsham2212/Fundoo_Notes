using CommonLayer.Note;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public Task AddNotes(NotePostModel notesModel, int userId);
        NotePostModel UpdateNotes(NotePostModel notes, int NoteId);
        public bool DeleteNotes(int NoteId);
        Task<List<Note>> GetAllNotes();
        public Task Color(int NoteId, string color);
        public Task ArchieveNote(int NoteId, int userId);
        public Task PinNote(int NoteId,int userId);
        public Task TrashNote(int NoteId, int userId);
        public IEnumerable<Note> GetAllNotesByNoteId(int NoteId);
    }  
}
