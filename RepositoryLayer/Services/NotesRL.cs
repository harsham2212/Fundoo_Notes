using CommonLayer.Note;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesRL : INoteRL
    {
        FundooDBContext dbContext;
        public NotesRL(FundooDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddNotes(NotePostModel notesModel, int userId)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(x => x.userId == userId);
                Note note = new Note();
                note.noteId = new Note().noteId;
                note.userId = userId;
                note.Title = notesModel.Title;
                note.Description = notesModel.Description;
                note.IsArchive = false;
                note.IsReminder = false;
                note.Color = notesModel.Color;
                note.IsPin = false;
                note.CreateDate = DateTime.Now;

                dbContext.Notes.Add(note);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public NotePostModel UpdateNotes(NotePostModel notes, int NoteId)
        {
            try
            {
                var UpdateNote = this.dbContext.Notes.Where(Y => Y.noteId == NoteId).FirstOrDefault();
                if (UpdateNote != null)
                {
                    UpdateNote.Title = notes.Title;
                    UpdateNote.Description = notes.Description;
                    UpdateNote.ModifiedDate = DateTime.Now;
                }
                var result = this.dbContext.SaveChanges();
                if (result > 0)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNotes(int NoteId)
        {
            try
            { 
                var ValidNote = this.dbContext.Notes.Where(Y => Y.noteId == NoteId).FirstOrDefault();
                this.dbContext.Notes.Remove(ValidNote);
                int result = this.dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Note>> GetAllNotes()
        {
            return await dbContext.Notes.ToListAsync();
        }

        public async Task ArchieveNote(int noteId, int userId)
        {
            try
            {
                var note = dbContext.Notes.FirstOrDefault(u => u.noteId == noteId && u.userId ==userId);
                note.IsArchive = true;
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Color(int NoteId, string color)
        {
            try
            {
                var note = dbContext.Notes.FirstOrDefault(u => u.noteId == NoteId);
                note.Color = color;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task PinNote(int NoteId,int userId)
        {
            try
            {
                var note = dbContext.Notes.FirstOrDefault(x => x.noteId == NoteId && x.userId== userId);
                if (note != null)
                {
                    note.IsPin = true;
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task TrashNote(int NoteId,int userId)
        {
            try
            {
                Note note = dbContext.Notes.FirstOrDefault(e => e.noteId == NoteId && e.userId ==userId);
                if (note != null)
                {
                    note.IsTrash = true;
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Note> GetAllNotesByNoteId(int NoteId)
        {
            return dbContext.Notes.Where(Y => Y.noteId == NoteId).ToList();
        }
    }   
}

 

