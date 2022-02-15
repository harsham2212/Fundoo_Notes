using CommonLayer.Note;
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
        public IEnumerable<Note> GetAllNotes();
        public Task Color(int NoteId, string color);
        public Task ArchieveNote(int NoteId);
        public Task PinNote(int NoteId);
        public Task TrashNote(int NoteId);
        public IEnumerable<Note> GetAllNotesByNoteId(int NoteId);
    }  
}
