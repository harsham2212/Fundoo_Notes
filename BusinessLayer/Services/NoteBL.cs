using BusinessLayer.Interface;
using CommonLayer.Note;
using DocumentFormat.OpenXml.ExtendedProperties;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INotesBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public async Task AddNotes(NotePostModel notesModel, int userId)
        {
            try
            {
                await noteRL.AddNotes(notesModel, userId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool DeleteNotes(int NoteId)
        {
            try
            {
                return noteRL.DeleteNotes(NoteId);
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
                return noteRL.UpdateNotes(notes, NoteId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public IEnumerable<Note> GetAllNotes()
        {
            try
            {
                return noteRL.GetAllNotes();
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
                await noteRL.Color(NoteId, color);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ArchieveNote(int NoteId)
        {
            try
            {
                await noteRL.ArchieveNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task PinNote(int NoteId)
        {
            try
            {
                await noteRL.PinNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task TrashNote(int NoteId)
        {
            try
            {
                await noteRL.TrashNote(NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Note> GetAllNotesByNoteId(int NoteId)
        {
            try
            {
                return noteRL.GetAllNotesByNoteId(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
 
