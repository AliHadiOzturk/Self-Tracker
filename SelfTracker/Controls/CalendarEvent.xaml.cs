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
        public static BindableProperty CalendarEventCommandProperty =
            BindableProperty.Create(nameof(CalendarEventCommand), typeof(ICommand), typeof(CalendarEvent), null);

        public static BindableProperty DeleteEventCommandProperty =
            BindableProperty.Create(nameof(DeleteEventCommand), typeof(IAsyncCommand<Event>), typeof(CalendarEvent), null);
        public CalendarEvent()
        {
            InitializeComponent();
        }

        public ICommand CalendarEventCommand
        {
            get => (ICommand)GetValue(CalendarEventCommandProperty);
            set => SetValue(CalendarEventCommandProperty, value);
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
                CalendarEventCommand?.Execute(eventModel);
            }
        }

        private async void SwipeItem_Invoked(object sender, System.EventArgs e)
        {
            if (BindingContext is Event eventModel)
                await DeleteEventCommand?.ExecuteAsync(eventModel);

        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            swipeView.Open(OpenSwipeItem.LeftItems);
        }
    }
}