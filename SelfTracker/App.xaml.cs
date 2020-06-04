using MvvmHelpers;
using SelfTracker.Helpers;
using SelfTracker.Services;
using SelfTracker.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfTracker
{
    public partial class App : Application
    {
        public App()
        {
            VersionTracking.Track();

            Device.SetFlags(new[] { "SwipeView_Experimental" });

            DependencyService.Register<DataService>();
            DependencyService.Register<SQLiteHelper>();
            DependencyService.Register<YearService>();
            DependencyService.Register<MonthService>();
            DependencyService.Register<DayService>();
            DependencyService.Register<EventService>();
            InitSQLite();


            InitializeComponent();
            MainPage = new NavigationPage(new CalendarPage());
        }

        public void InitSQLite()
        {
            DependencyService.Get<SQLiteHelper>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
