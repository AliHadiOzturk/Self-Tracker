using MvvmHelpers;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Plugin.LocalNotification;
using SelfTracker.Helpers;
using SelfTracker.Models;
using SelfTracker.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfTracker.ViewModels
{
    public class AddReminderViewModel : BaseViewModel
    {
        EventService EventService;

        DataService DataService;

        public Page _page { get; set; }

        public IAsyncCommand SaveEventCommand => new AsyncCommand(SaveEvent);



        private Event _event;

        public Event Event
        {
            get { return _event; }
            set { SetProperty(ref _event, value); }
        }


        public DateTime DateToSave { get; set; }


        private String _selectedDate;

        public String SelectedDate
        {
            get { return _selectedDate; }
            set { SetProperty(ref _selectedDate, value); }
        }

        private TimeSpan _time;

        public TimeSpan Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); Event.Time = new DateTime(DateToSave.Year, DateToSave.Month, DateToSave.Day, Time.Hours, Time.Minutes, 0); }
        }

        public AddReminderViewModel()
        {
            Init();
            Event = new Event();
            Event.IsReminder = true;
            var date = DateTime.Now.AddHours(1);
            Event.Time = date;
            Time = new TimeSpan(date.Hour, date.Minute, 0);
        }
        public void Init()
        {
            EventService = DependencyService.Get<EventService>();
            DataService = DependencyService.Get<DataService>();
        }

        private async Task SaveEvent()
        {
            if (ValidationHelper.IsFormValid(Event, _page))
            {
                await DataService.CreateRelationsAndSaveEvent(Event, DateToSave);
                var lastSavedEvent = await EventService.GetLastSavedEvent();
                var notification = new NotificationRequest
                {
                    NotificationId = lastSavedEvent.Id,
                    Title = lastSavedEvent.Name,
                    Description = lastSavedEvent.Description,
                    Android =
                    {
                        IconName = "watermelon",
                        ChannelId="SelfTrackerChannelId",
                    },
                    //ReturningData = "Dummy data", // Returning data when tapped on notification.
                    NotifyTime = Event.Time // Used for Scheduling local notification, if not specified notification will show immediately.
                };
                NotificationCenter.Current.Show(notification);
                //CrossLocalNotifications.Current.Show(Event.Name, Event.Description, lastSavedEvent.Id, Event.Time);
                await App.Current.MainPage.Navigation.PopModalAsync(true);
            }
        }
    }
}
