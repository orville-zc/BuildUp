using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Data
{
    public class ActionServices
    {
        private ApplicationDbContext _context;

        public ActionServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Action>> GetActionsAsync()
        {
            return await _context.Actions
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<int> AddActionAsync(Action action)
        {
            try
            {
                _context.Actions.Add(action);
                await _context.SaveChangesAsync();
                return action.ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<bool> UpdateActionAsync(Action action)
        {
            try
            {
                var actionToUpdate = await _context.Actions.FindAsync(action.ID);

                if (actionToUpdate != null && actionToUpdate.RowVersion.SequenceEqual(action.RowVersion))
                {
                    actionToUpdate.Name = action.Name;
                    actionToUpdate.Note = action.Note;
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteActionAsync(int id)
        {
            var action = await _context.Actions.FindAsync(id);
            if (action == null)
            {
                return false;
            }
            try
            {
                _context.Actions.Remove(action);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
