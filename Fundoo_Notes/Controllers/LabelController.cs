using BusinessLayer.Interface;
using CommonLayer.Label;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_Notes.Controllers
{
    [ApiController]
    [Route("Label")]

    public class LabelController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooDBContext fundooDBContext;
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL, FundooDBContext fundooDB, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.fundooDBContext = fundooDB;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost ("createLabel/{noteId}")]
        public async Task<IActionResult> CreateLabel(LabelModel labelModel, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                await labelBL.CreateLabel(labelModel, userId, noteId);
                return this.Ok(new { success = true, message = "Label added successfully", response =  noteId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("updateLabel/{labelId}")]

        public IActionResult UpdateNotes(int labelId, LabelModel labelModel)
        {
            try
            {
                if (labelBL.UpdateLabel(labelId, labelModel))
                {
                    return this.Ok(new { Success = true, message = "Labels updated successfully", response = labelId, labelModel });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note with given ID not found" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("deletelabel/{labelId}")]
        public IActionResult DeleteLabel(int labelId)
        {
            try
            {
                if (labelBL.DeleteLabel(labelId))
                {
                    return this.Ok(new { Success = true, message = "Labels deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Labels with UserID not found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetAllLabels")]
        public async Task<IActionResult> GetAllLabels()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var LabelList = new List<Label>();
                var NoteList = new List<Note>();
                LabelList = await labelBL.GetAllLabels(userId);

                return this.Ok(new { Success = true, message = $"GetAll Labels of UserId={userId} ", data = LabelList });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetLabelsByNoteID/{noteId}")]
        public async Task<IActionResult> GetLabelsByNoteID(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var LabelList = new List<Label>();
                var NoteList = new List<Note>();
                LabelList = await labelBL.GetLabelsByNoteID(userId, noteId);

                return this.Ok(new { Success = true, message = $"GetAll Labels of NoteId={noteId} ", data = LabelList });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
