using BusinessLayer.Interface;
using CommonLayer.Collabarator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_Notes.Controllers
{
    [ApiController]
    [Route("collabrator")]
    public class CollabratorController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooDBContext fundooDBContext;
        ICollabBL CollabratorBL;
        public CollabratorController(ICollabBL CollabratorBL, FundooDBContext fundooDB, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.CollabratorBL = CollabratorBL;
            this.fundooDBContext = fundooDB;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost("addCollabartor/{noteId}")]
        public async Task<IActionResult> AddCollabrator(int noteId, CollabModel postModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await CollabratorBL.AddCollabrator(UserId, noteId, postModel);

                return this.Ok(new { success = true, message = "Collabartion added successfully", response = noteId, postModel });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpDelete("deleteCollabs/{CollabId}")]
        public async Task<IActionResult> RemoveCollabs(int CollabId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Userid", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);

                await CollabratorBL.RemoveCollabrator(CollabId, UserId);
                return this.Ok(new { success = true, message = "Collabartion deleted successfully", response = CollabId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpGet("getallCollaborators")]
        public async Task<IActionResult> GetAllCollaborators()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                List<Collabarator> collab = new List<Collabarator>();
                collab = await CollabratorBL.GetAllCollaborators(UserId);
                return this.Ok(new { success = true, message = "  Get All Collaborators from User ", response = collab });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
