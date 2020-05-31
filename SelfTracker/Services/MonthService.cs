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
    public class MonthService
    {
        private SQLiteAsyncConnection Connection { get; set; }
        public MonthService()
        {
            Connection = DependencyService.Get<SQLiteHelper>().Connection;
        }

        public async Task<MonthModel> Save(MonthModel wrapper)
        {
            try
            {
                await Connection.InsertAsync(wrapper);
                return await Connection.Table<MonthModel>().Where(item => item.YearId == wrapper.YearId && item.Month == wrapper.Month).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<MonthModel> GetByMonthAndYearId(int month, int yearId)
        {
            var wrapper = await Connection.Table<MonthModel>().Where(item => item.Month == month && item.YearId == yearId).FirstOrDefaultAsync();
            //if (wrapper == null)
            //{
            //    var id = await Connection.InsertAsync(new MonthWrapper() { Month = month, YearId = yearId });
            //    wrapper = await Connection.Table<MonthWrapper>().Where(item => item.Id == id).FirstOrDefaultAsync();
            //}
            return wrapper;
        }
    }
}
