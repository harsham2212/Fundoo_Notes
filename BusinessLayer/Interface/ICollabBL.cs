using CommonLayer.Collabarator;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public interface ICollabBL
    {
        Task<List<Collabarator>> AddCollabrator(int userId, int noteId, CollabModel postModel);
        public Task RemoveCollabrator(int CollabId, int userId);
        public Task<List<Collabarator>> GetAllCollaborators(int userId);
    }
}
