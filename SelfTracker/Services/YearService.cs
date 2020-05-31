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
    public class YearService
    {
        private SQLiteAsyncConnection Connection { get; set; }
        public YearService()
        {
            Connection = DependencyService.Get<SQLiteHelper>().Connection;
        }
        public async Task<YearModel> Save(YearModel wrapper)
        {
            try
            {
                await Connection.InsertAsync(wrapper);
                return await Connection.Table<YearModel>().Where(item => item.Year == wrapper.Year).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<YearModel> GetByYear(int year)
        {
            var wrapper = await Connection.Table<YearModel>().Where(item => item.Year == year).FirstOrDefaultAsync();
            //if (wrapper == null)
            //{
            //    var id = await Connection.InsertAsync(new YearWrapper() { Year = year });
            //    wrapper = await Connection.Table<YearWrapper>().Where(item => item.Id == id).FirstOrDefaultAsync();
            //}
            return wrapper;
        }


    }
}
