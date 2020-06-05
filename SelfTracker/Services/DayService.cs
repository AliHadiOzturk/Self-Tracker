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
    public class DayService
    {
        private SQLiteAsyncConnection Connection { get; set; }
        public DayService()
        {
            Connection = DependencyService.Get<SQLiteHelper>().Connection;
        }

        public async Task<DayModel> Save(DayModel wrapper)
        {
            try
            {
                await Connection.InsertAsync(wrapper);
                return await Connection.Table<DayModel>().Where(item => item.Day == wrapper.Day && item.MonthId == wrapper.MonthId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<DayModel> GetById(int id) => await Connection.Table<DayModel>().Where(item => item.Id == id).FirstOrDefaultAsync();

        public async Task SetEmoji(string emoji, int dayId)
        {
            var day = await Connection.Table<DayModel>().Where(item => item.Id == dayId).FirstOrDefaultAsync();
            day.Emoji = emoji;
            var i = await Connection.UpdateAsync(day, typeof(DayModel));
        }

        public async Task<List<DayModel>> GetFirstDaysByMonthId(int monthId)
            => await Connection.Table<DayModel>().Where(item => item.MonthId == monthId && item.Day < 10).ToListAsync();

        public async Task<List<DayModel>> GetLasyDaysByMonthId(int monthId)
            => await Connection.Table<DayModel>().Where(item => item.MonthId == monthId && item.Day > 20).ToListAsync();
        public async Task<List<DayModel>> GetByMonthId(int monthId)
            => await Connection.Table<DayModel>().Where(item => item.MonthId == monthId).ToListAsync();
        public async Task<DayModel> GetByDayAndMonthId(int day, int monthId)
        {
            var wrapper = await Connection.Table<DayModel>().Where(item => item.Day == day && item.MonthId == monthId).FirstOrDefaultAsync();
            //if (wrapper == null)
            //{
            //    var id = await Connection.InsertAsync(new DayWrapper() { Day = day, MonthId = monthId, Date = new DateTime(date.Value.Year, date.Value.Month, day) });
            //    wrapper = await Connection.Table<DayWrapper>().Where(item => item.Id == id).FirstOrDefaultAsync();
            //}
            return wrapper;
        }
    }
}
