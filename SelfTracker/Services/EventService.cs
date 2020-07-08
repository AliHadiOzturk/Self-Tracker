using SelfTracker.Helpers;
using SelfTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfTracker.Services
{
    public class EventService
    {
        private SQLiteAsyncConnection Connection { get; set; }
        public EventService()
        {
            Connection = DependencyService.Get<SQLiteHelper>().Connection;
        }

        public async Task<int> Save(Event eventData)
        {
            try
            {
                return await Connection.InsertAsync(eventData);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public async Task Delete(Event _event) => await Connection.Table<Event>().DeleteAsync(item => item.Id == _event.Id);
        public async Task<Event> GetByDayId(int dayId)
        {
            return await Connection.Table<Event>().Where(item => item.DayId == dayId).FirstOrDefaultAsync();
        }
        public async Task<List<Event>> GetAllByDayId(int dayId)
        {
            return await Connection.Table<Event>().Where(e => e.DayId == dayId).ToListAsync();
        }

        public async Task<Event> GetLastSavedEvent() => await Connection.Table<Event>().OrderByDescending(item => item.Id).FirstOrDefaultAsync();

        public async Task<List<Event>> GetAll() => await Connection.Table<Event>().ToListAsync();
    }
}
