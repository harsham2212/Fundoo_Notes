using CommonLayer.Collabarator;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollabratorRL: ICollabRL
    {
        FundooDBContext dbContext;

        public CollabratorRL(FundooDBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<List<Collabarator>> AddCollabrator(int userId, int noteId, CollabModel postModel)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(e => e.userId == userId);
                var note = dbContext.Notes.FirstOrDefault(u => u.noteId == noteId);

                Collabarator collabarator = new Collabarator();
                collabarator.userId = userId;
                collabarator.noteId = noteId;
                collabarator.CollabId = new Collabarator().CollabId;
                collabarator.CollabEmail = postModel.CollabEmail;
                collabarator.User = user;
                collabarator.Notes = note;
                dbContext.collab.Add(collabarator);
                await dbContext.SaveChangesAsync();
                return await dbContext.collab.Where(u => u.userId == userId)
                    .Include(u => u.User)
                    .Include(u => u.Notes)
                     .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RemoveCollabrator(int CollabId, int userId)
        {
            try
            {
                Collabarator collabarator = await dbContext.collab.Where(u => u.CollabId == CollabId).FirstOrDefaultAsync();
                if (collabarator != null)
                {
                    this.dbContext.collab.Remove(collabarator);
                    await this.dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Collabarator>> GetAllCollaborators(int userId)
        {
            try
            {
                Collabarator collabarator = new Collabarator();
                return await dbContext.collab.Where(u => u.userId == userId)
                    .Include(u => u.Notes)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
