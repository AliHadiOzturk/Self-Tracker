using MvvmHelpers;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using SelfTracker.Helpers;
using SelfTracker.Models;
using SelfTracker.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfTracker.ViewModels
{
    public class AddEventViewModel : BaseViewModel
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

        public AddEventViewModel()
        {
            Init();
            Event = new Event();
            Event.IsReminder = false;
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
                await App.Current.MainPage.Navigation.PopModalAsync(true);
            }
        }


    }
}
