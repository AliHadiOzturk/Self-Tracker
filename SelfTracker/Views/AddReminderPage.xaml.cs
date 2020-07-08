using MvvmHelpers;
using SelfTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReminderPage : ContentPage
    {
        public AddReminderPage(BaseViewModel vm)
        {
            InitializeComponent();
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#ff0037");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            BindingContext = vm;

            (BindingContext as AddReminderViewModel)._page = this;
        }
    }
}