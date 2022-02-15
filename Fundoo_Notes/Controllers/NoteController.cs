using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_Notes.Controllers
{
    [ApiController]
    [Route("Note")]

    public class NoteController : ControllerBase
    {
        FundooDBContext fundooDBContext;
        INotesBL NotesBL;
        public NoteController(INotesBL NotesBL, FundooDBContext fundooDB)
        {
            this.NotesBL = NotesBL;
            this.fundooDBContext = fundooDB;
        }

        [Authorize]
        [HttpPost("addnotes")]
        public async Task<ActionResult> AddNotes(NotePostModel notesModel)
        {
            try
                {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await this.NotesBL.AddNotes(notesModel, UserId);

                return this.Ok(new { success = true, message = $"Note Created Sucessfully" });
            }
                catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("updatenotes/{NoteId}")]
        public IActionResult UpdateNotes(NotePostModel notes, int NoteId)
        {
            try
            {
                var result = NotesBL.UpdateNotes(notes, NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Updating a note Sucessfull", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Update a note" });
                }
            }
            catch (System.Exception e )
            {
                throw e;
            }
        }

        [Authorize]
        [HttpDelete("deletenotes/{NoteId}")]
        public IActionResult DeleteNotes(int NoteId)
        {
            try
            {
                if (NotesBL.DeleteNotes(NoteId))
                {
                    return this.Ok(new { Success = true, message = "Note Deleted  Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to delete the note" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("getallnotes")]
        public IEnumerable<Note> GetAllNotes()
        {
            try
            {
                return NotesBL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("changeColor/{NoteId}/{color}")]
        public IActionResult Color(int NoteId, string color)
        {
            try
            {
                var result = NotesBL.Color(NoteId, color);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("pinnotes/{NoteId}")]
        public async Task<IActionResult> PinNotes(int NoteId)
        {
            try
            {
                var result = NotesBL.PinNote(NoteId);
                await NotesBL.PinNote(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("archiveNote/{NoteId}")]
        public async Task<IActionResult> ArchieveNotes(int NoteId)
        {
            try
            {
                var result = NotesBL.ArchieveNote(NoteId);
                await NotesBL.ArchieveNote(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Archieve changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("trash/{NoteId}")]
        public async Task<IActionResult> TrashNotes(int NoteId)
        {
            try
            {
                var result = NotesBL.TrashNote(NoteId);
                await NotesBL.TrashNote(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("getbynoteId/{NoteId}")]
        public IEnumerable<Note> GetAllNotesByUserId(int NoteId)
        {
            //int NoteId = (User.Claims.FirstOrDefault(x => x.Type == "NoteId").Value);
            try
            {
                return NotesBL.GetAllNotesByNoteId(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
