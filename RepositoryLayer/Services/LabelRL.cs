using CommonLayer.Label;
using CommonLayer.Note;
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
    public class LabelRL : ILabelRL
    {
        FundooDBContext dbContext;
        public LabelRL(FundooDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateLabel(LabelModel labelModel, int noteId, int userId)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(e => e.userId == userId);
                var note = dbContext.Notes.FirstOrDefault(u => u.noteId == noteId);

                Label labels = new Label();
                labels.userId = userId;
                labels.noteId = noteId;
                labels.LabelId = new Label().LabelId;
                labels.LabelName = labelModel.LabelName;
                dbContext.Label.Add(labels);
                await dbContext.SaveChangesAsync();
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
                Label label = dbContext.Label.Where(e => e.LabelId == LabelId).FirstOrDefault();
                label.LabelName = labelModel.LabelName;
                dbContext.Label.Update(label);
                var result = dbContext.SaveChangesAsync();
                if (result != null)
                {
                    return true;
                }
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
                Label label = dbContext.Label.Where(e => e.LabelId == LabelId).FirstOrDefault();
                if (label != null)
                {
                    dbContext.Label.Remove(label);
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        //public async Task<List<LabelResponse>> GetAllLabels(int userId)
        //{
        //    Label label = new Label();
        //    try
        //    {
        //        return await dbContext.Label.Where(l => l.userId == userId)
        //           .Join(dbContext.Users,
        //        l => l.userId,
        //        u => u.userId,
        //        (l, u) => new LabelResponse
        //        {
        //            userId = (int)l.userId,
        //            email = u.email,
        //            LabelName = l.LabelName,
        //            fname = u.fname,
        //            lname = u.lname,
        //            phoneNo=u.phoneNo
        //        }).ToListAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public async Task<List<LabelResponse>> GetAllLabels(int userId)
        {
            Label labels = new Label();
            try
            {
                return await dbContext.Label.Where(l => l.userId == userId)

                  .Join(dbContext.Users
                  .Join(dbContext.Notes,
                    u => u.userId,
                    n => n.userId,
                    (u, n) => new NoteUserResponse
                    {
                        userId = u.userId,
                        noteId = n.noteId,
                        fname = u.fname,
                        lname = u.lname,
                        RegisteredDate = n.CreateDate,
                        Title = n.Title,
                        Description = n.Description,
                        email = u.email,
                        phoneNo =u.phoneNo,

                    }),
                   l => l.Notes.noteId,
                    un => un.noteId,
                    (l, un) => new LabelResponse
                    {
                        userId = un.userId,
                        noteId = l.Notes.noteId,
                        Title = un.Title,
                        Description = un.Description,
                        RegisteredDate = un.RegisteredDate,
                        fname = un.fname,
                        lname = un.lname,
                        email = un.email,
                        phoneNo = un.phoneNo,
                        LabelName = l.LabelName

                    }).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetLabelsByNoteID(int userId, int noteId)
        {
            try
            {
                return await dbContext.Label.Where(e => e.noteId == noteId && e.userId == userId)
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

