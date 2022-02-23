using BusinessLayer.Interface;
using CommonLayer.Collabarator;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabratorBL : ICollabBL
    {
        ICollabRL collabratorRL;
        public CollabratorBL(ICollabRL collabratorRL)
        {
            this.collabratorRL = collabratorRL;
        }

        public async Task<List<Collabarator>> AddCollabrator(int userId, int noteId, CollabModel postModel)
        {
            try
            {
                return await collabratorRL.AddCollabrator(userId, noteId, postModel);
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
                await collabratorRL.RemoveCollabrator(CollabId, userId);
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
                return await collabratorRL.GetAllCollaborators(userId);
            }
            catch (Exception e)
            { 
                throw e;
            }
        }
    }
}
