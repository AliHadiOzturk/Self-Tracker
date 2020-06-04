using MvvmHelpers;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using SelfTracker.Controls;
using SelfTracker.Models;
using SelfTracker.Services;
using SelfTracker.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace SelfTracker.ViewModels
{
    public class CalendarPageViewModel : BaseViewModel
    {
        public DayService DayService { get; set; }
        public MonthService MonthService { get; set; }
        public YearService YearService { get; set; }
        public EventService EventService { get; set; }
        public DataService DataService { get; set; }

        public ICommand DayTappedCommand => new Xamarin.Forms.Command((date) => { YearPickerVisible = false; });

        public IAsyncCommand SwipeLeftCommand => new AsyncCommand(SwipeLeft);
        public IAsyncCommand SwipeRightCommand => new AsyncCommand(SwipeRight);
        public ICommand SwipeUpCommand => new Xamarin.Forms.Command(() => { MonthYear = DateTime.Today; });
        public IAsyncCommand AddEventCommand => new AsyncCommand(AddEvent);
        public IAsyncCommand ChangeEmojiCommand => new AsyncCommand(ChangeEmoji);



        public IAsyncCommand<Event> DeleteEventCommand => new AsyncCommand<Event>(DeleteEvent);
        public IAsyncCommand ChangeDateCommand => new AsyncCommand(ChangeDate);



        public EventCollection Events { get; }
        private DateTime _monthYear = DateTime.Today;
        public DateTime MonthYear
        {
            get => _monthYear;
            set => SetProperty(ref _monthYear, value);
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }


        private CultureInfo _culture = CultureInfo.CurrentCulture;
        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        private bool _yearPickerVisible = false;

        public bool YearPickerVisible
        {
            get { return _yearPickerVisible; }
            set { SetProperty(ref _yearPickerVisible, value); }
        }

        private string _emoji;

        public string Emoji
        {
            get { return _emoji; }
            set { SetProperty(ref _emoji, value); }
        }


        public CalendarPageViewModel()
        {
            Events = new EventCollection();
            Init();
        }


        private async void Init()
        {
            DataService = DependencyService.Get<DataService>();
            EventService = DependencyService.Get<EventService>();
            await GetEvents();

        }

        private async Task GetEvents()
        {
            Events.Clear();
            var dataWrappers = await DataService.GetEvents(this.MonthYear);
            foreach (var data in dataWrappers)
            {
                Events.Add(data.Day.Date, data.Events);
            }
        }

        private async Task AddEvent()
        {
            var vm = new AddEventViewModel()
            {
                DateToSave = this.SelectedDate,
                SelectedDate = this.SelectedDate.ToString("dd/MM/yyyy")
            };
            var pageToLoad = new AddEventPage(vm);
            pageToLoad.Disappearing += async (sender, args) => await GetEvents();

            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(pageToLoad), true);
        }

        private async Task DeleteEvent(Event data)
        {
            await EventService.Delete(data);
            await GetEvents();
        }

        private async Task ChangeEmoji()
        {
            var result = await App.Current.MainPage.DisplayPromptAsync("Emoji seçiniz", "Günün mood'unu seçiniz :)", cancel: "Kapat", placeholder: "Emoji", maxLength: 7, keyboard: Keyboard.Chat);
            if (!string.IsNullOrEmpty(result))
                MessagingCenter.Send<CalendarPageViewModel, string>(this, "EmojiChanged", result);
            //Emoji = result;
        }

        private Task ChangeDate()
        {
            YearPickerVisible = !YearPickerVisible;
            return Task.CompletedTask;
        }

        private async Task SwipeLeft()
        {
            MonthYear = MonthYear.AddMonths(1);
            await GetEvents();
        }
        private async Task SwipeRight()
        {
            MonthYear = MonthYear.AddMonths(-1);
            await GetEvents();
        }

        //private async Task DayTapped(DateTime date)
        //{
        //}

        //private async Task ExecuteEventSelectedCommand(object item)
        //{
        //    if (item is Event eventModel)
        //    {
        //        var title = $"Selected: {eventModel.Name}";
        //        var message = $"Starts: {eventModel.Starting:HH:mm}{Environment.NewLine}Details: {eventModel.Description}";
        //        await App.Current.MainPage.DisplayAlert(title, message, "Ok");
        //        Console.WriteLine(message);
        //    }
        //}
    }
}
