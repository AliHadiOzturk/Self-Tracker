using MvvmHelpers.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfTracker.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarHeader : ContentView
    {
        public static BindableProperty ChangeDateCommandProperty =
            BindableProperty.Create(nameof(ChangeDateCommand), typeof(IAsyncCommand), typeof(CalendarHeader), null);

        //public static BindableProperty PrevMonthCommand

        public IAsyncCommand ChangeDateCommand
        {
            get => (IAsyncCommand)GetValue(ChangeDateCommandProperty);
            set => SetValue(ChangeDateCommandProperty, value);
        }
        public CalendarHeader()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await ChangeDateCommand?.ExecuteAsync();
        }
    }
}