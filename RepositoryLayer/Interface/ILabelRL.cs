using CommonLayer.Label;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task CreateLabel(LabelModel labelModel, int noteId, int userId);
        public bool UpdateLabel(int LabelId, LabelModel labelModel);
        Task<List<Label>> GetAllLabels(int userId);
        public Task<List<Label>> GetLabelsByNoteID(int userId, int NoteId);
        public bool DeleteLabel(int LabelId);
    }
}
