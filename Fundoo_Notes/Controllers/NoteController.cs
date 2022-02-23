using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundoo_Notes.Controllers
{
    [ApiController]
    [Route("note")]

    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooDBContext fundooDBContext;
        INotesBL NotesBL;
        public NoteController(INotesBL NotesBL, FundooDBContext fundooDB, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.NotesBL = NotesBL;
            this.fundooDBContext = fundooDB;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost("addnotes")]
        public async Task<ActionResult> AddNotes(NotePostModel notesModel)
        {
            try
                {
                //var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                //int UserId = Int32.Parse(userId.Value);
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                await this.NotesBL.AddNotes(notesModel, userId);

                return this.Ok(new { success = true, message = $"Note Created Sucessfully" });
            }
                catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("updateNotes/{NoteId}")]
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
        [HttpDelete("deleteNotes/{NoteId}")]
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

        [Authorize]
        [HttpGet("getAllNoteUsingRedis")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var cacheKey = "NoteList";
                string serializedNoteList;
                var noteList = new List<Note>();
                var redisnoteList = await distributedCache.GetAsync(cacheKey);
                if (redisnoteList != null)
                {
                    serializedNoteList = Encoding.UTF8.GetString(redisnoteList);
                    noteList = JsonConvert.DeserializeObject<List<Note>>(serializedNoteList);
                }
                else
                {
                    noteList = await NotesBL.GetAllNotes();
                    serializedNoteList = JsonConvert.SerializeObject(noteList);
                    redisnoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                }
                return this.Ok(noteList);
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
                    return this.Ok(new { Success = true, message = "Color changed successfully"});
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
        [HttpPut("pinNotes/{NoteId}/{userId}")]
        public async Task<IActionResult> PinNotes(int NoteId,int userId)
        {
            try
            {
                var result = NotesBL.PinNote(NoteId,userId);
                await NotesBL.PinNote(NoteId,userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully"});
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
        [HttpPut("archiveNote/{NoteId}/{userId}")]
        public async Task<IActionResult> ArchieveNotes(int NoteId,int userId)
        {
            try
            {
                var result = NotesBL.ArchieveNote(NoteId,userId);
                await NotesBL.ArchieveNote(NoteId,userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Archieve changed successfully"});
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
        [HttpPut("trash/{NoteId}/{userId}")]
        public async Task<IActionResult> TrashNotes(int NoteId, int userId)
        {
            try
            {
                var result = NotesBL.TrashNote(NoteId,userId);
                await NotesBL.TrashNote(NoteId,userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully"});
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
        [HttpGet("getbynoteId/{noteId}")]
        public IEnumerable<Note> GetAllNotesByUserId(int noteId)
        {
            //int NoteId = (User.Claims.FirstOrDefault(x => x.Type == "NoteId").Value);
            try
            {
                return NotesBL.GetAllNotesByNoteId(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
