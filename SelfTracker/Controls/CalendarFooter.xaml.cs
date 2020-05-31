using MvvmHelpers.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfTracker.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarFooter : ContentView
    {
        public static BindableProperty AddEventCommandProperty =
            BindableProperty.Create(nameof(AddEventCommand), typeof(IAsyncCommand), typeof(CalendarFooter), null);
        public CalendarFooter()
        {
            InitializeComponent();
        }

        public IAsyncCommand AddEventCommand
        {
            get => (IAsyncCommand)GetValue(AddEventCommandProperty);
            set => SetValue(AddEventCommandProperty, value);
        }


        private async void AddEvent_Tapped(object sender, System.EventArgs e)
        {
            await AddEventCommand?.ExecuteAsync();
        }
    }
}