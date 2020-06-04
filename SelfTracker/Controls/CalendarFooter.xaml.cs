using MvvmHelpers.Interfaces;
using SelfTracker.ViewModels;
using System;
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

        public static BindableProperty ChangeEmojiCommandProperty =
            BindableProperty.Create(nameof(ChangeEmojiCommand), typeof(IAsyncCommand), typeof(CalendarFooter), null);

        public static readonly BindableProperty EmojiProperty = BindableProperty.Create(nameof(Emoji), typeof(string), typeof(CalendarFooter), "☺");



        public CalendarFooter()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<CalendarPageViewModel, string>(this, "EmojiChanged", (sender, args) => Emoji = args);
        }

        public IAsyncCommand AddEventCommand
        {
            get => (IAsyncCommand)GetValue(AddEventCommandProperty);
            set => SetValue(AddEventCommandProperty, value);
        }
        public IAsyncCommand ChangeEmojiCommand
        {
            get => (IAsyncCommand)GetValue(ChangeEmojiCommandProperty);
            set => SetValue(ChangeEmojiCommandProperty, value);
        }
        public string Emoji
        {
            get => (string)GetValue(EmojiProperty);
            set => SetValue(EmojiProperty, value);
        }

        private async void AddEvent_Tapped(object sender, System.EventArgs e)
        {
            await AddEventCommand?.ExecuteAsync();
        }

        private async void EmojiChange_Tapped(object sender, EventArgs e)
        {
            await ChangeEmojiCommand?.ExecuteAsync();
        }
    }
}