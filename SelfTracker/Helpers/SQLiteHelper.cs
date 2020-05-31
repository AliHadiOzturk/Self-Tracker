using MvvmHelpers;
using SelfTracker.Config;
using SelfTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SelfTracker.Helpers
{
    public class SQLiteHelper
    {
        static SQLiteAsyncConnection con;
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DBConsts.DatabasePath, DBConsts.Flags);
            //return con;
        });

        public SQLiteAsyncConnection Connection => con;
        //static bool initialized = false;

        public SQLiteHelper()
        {
            con = lazyInitializer.Value;
            if (VersionTracking.IsFirstLaunchEver)
                InitializeAsync().SafeFireAndForget(continueOnCapturedContext: false);
        }
        async Task InitializeAsync()
        {
            //if (!initialized)
            //{
            if (!Connection.TableMappings.Any(m => m.MappedType.Name == typeof(Event).Name))
            {
                await Connection.CreateTablesAsync(CreateFlags.None, typeof(YearModel)).ConfigureAwait(false);
                await Connection.CreateTablesAsync(CreateFlags.None, typeof(MonthModel)).ConfigureAwait(false);
                await Connection.CreateTablesAsync(CreateFlags.None, typeof(DayModel)).ConfigureAwait(false);
                await Connection.CreateTablesAsync(CreateFlags.None, typeof(Event)).ConfigureAwait(false);
                //initialized = true;
            }
            //}
        }
    }
}
