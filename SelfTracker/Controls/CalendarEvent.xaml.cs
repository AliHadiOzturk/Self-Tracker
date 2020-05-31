using MvvmHelpers.Interfaces;
using SelfTracker.Models;
using SelfTracker.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfTracker.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarEvent : ContentView
    {
        public static BindableProperty CalenderEventCommandProperty =
            BindableProperty.Create(nameof(CalenderEventCommand), typeof(ICommand), typeof(CalendarEvent), null);

        public static BindableProperty DeleteEventCommandProperty =
            BindableProperty.Create(nameof(DeleteEventCommand), typeof(IAsyncCommand<Event>), typeof(CalendarEvent), null);
        public CalendarEvent()
        {
            InitializeComponent();
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }

        public IAsyncCommand<Event> DeleteEventCommand
        {
            get => (IAsyncCommand<Event>)GetValue(DeleteEventCommandProperty);
            set => SetValue(DeleteEventCommandProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (BindingContext is Event eventModel)
            {
                CalenderEventCommand?.Execute(eventModel);
            }
        }

        private async void SwipeItem_Invoked(object sender, System.EventArgs e)
        {
            if (BindingContext is Event eventModel)
                await DeleteEventCommand?.ExecuteAsync(eventModel);

        }
    }
}