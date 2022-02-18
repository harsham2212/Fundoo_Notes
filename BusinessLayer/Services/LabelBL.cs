using BusinessLayer.Interface;
using CommonLayer.Label;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task CreateLabel(LabelModel labelModel, int userId, int NoteId)
        {
            try
            {
                await labelRL.CreateLabel(labelModel, userId, NoteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateLabel(int LabelId, LabelModel labelModel)
        {
            try
            {
                if (labelRL.UpdateLabel(LabelId, labelModel))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteLabel(int LabelId)
        {
            try
            {
                if (labelRL.DeleteLabel(LabelId))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetAllLabels(int userId)
        {
            try
            {
                return await labelRL.GetAllLabels(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetLabelsByNoteID(int userId, int NoteId)
        {
            try
            {
                return await labelRL.GetLabelsByNoteID(userId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
