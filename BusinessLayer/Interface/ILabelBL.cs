using CommonLayer.Label;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public Task CreateLabel(LabelModel labelModel, int noteId, int userId);
        public bool UpdateLabel(int LabelId, LabelModel labelModel);
        public Task<List<Label>> GetLabelsByNoteID(int userId, int NoteId);
        public Task<List<LabelResponse>> GetAllLabels(int userId);
        public bool DeleteLabel(int LabelId);
    }
}
