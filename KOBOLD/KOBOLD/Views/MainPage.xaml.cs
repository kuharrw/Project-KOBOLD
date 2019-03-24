using KOBOLD.Models;
using KOBOLD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KOBOLD.Views
{
    public partial class MainPage : ContentPage
    {
        protected MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();

            vm = new MainPageViewModel();
            this.BindingContext = vm;
        }

        /// <summary>
        /// Is called any time the screen is brought up.
        /// Calls the refresh method to ensure that fresh data is presented every time the page is loaded
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.Refresh();
        }

        /// <summary>
        /// Tapped event for the event list. Will open the tapped event's sign in sheet
        /// </summary>
        private void LstEvents_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new SignInSheetPage(e.Item as Event));
        }

        /// <summary>
        /// Click event for the event add button. Will open a blank event page.
        /// </summary>
        private void BtnAddEvent_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EventPage());
        }

        /// <summary>
        /// Menu command for editing the event, achieved by long tapping an event, then selecting edit from the menu.
        /// will open the event's details.
        /// </summary>
        private void EventEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new EventPage((mi.CommandParameter as Event)));
        }
    }
}
