using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildUp.Data
{
    public class TimeEntryServices
    {
        private ApplicationDbContext _context;

        public TimeEntryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TimeEntry> GetLastTimeEntryByIdAsync(int actionId)
        {
            var timeEntries = _context.TimeEntries
                .Where(t => t.ActionID == actionId)
                .OrderByDescending(t => t.TimeStart);

            if (timeEntries.Any())
            {
                return await timeEntries.FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByActionIdAsync(int actionId)
        {
            return await _context.TimeEntries.Where(t => t.ActionID == actionId).ToListAsync();
        }

        public async Task<int> AddTimeEntryAsync(TimeEntry timeEntry)
        {
            try
            {
                _context.TimeEntries.Add(timeEntry);
                await _context.SaveChangesAsync();
                return timeEntry.ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<bool> UpdateTimeEntryAsync(int id, DateTime now)
        {
            try
            {
                var timeEntryToUpdate = await _context.TimeEntries.FindAsync(id);

                if (timeEntryToUpdate != null && !timeEntryToUpdate.TimeEnd.HasValue)
                {
                    timeEntryToUpdate.TimeEnd = now;
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

        public async Task<bool> DeleteTimeEntryAsync(int id)
        {
            var timeEntry = await _context.TimeEntries.FindAsync(id);
            if (timeEntry == null)
            {
                return false;
            }
            try
            {
                _context.TimeEntries.Remove(timeEntry);
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
