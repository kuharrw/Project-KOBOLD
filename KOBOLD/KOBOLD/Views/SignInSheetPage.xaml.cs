using KOBOLD.Models;
using KOBOLD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KOBOLD.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInSheetPage : ContentPage
	{
        SignInSheetPageViewModel vm;
        public SignInSheetPage()
        {
            InitializeComponent();
        }

		public SignInSheetPage (Event @event)
		{
			InitializeComponent ();
            Title = @event.Name;
            vm = new SignInSheetPageViewModel(@event);
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
            this.Title = vm.Event.Name + $" ({vm.SignIns.Count} Total Sign Ins.";

            if(vm.Event.IsEvent)
                this.Title += $"{ vm.SignIns.Where(s => !s.PayExempt).Count()} Paying Sign Ins)";
        }

        /// <summary>
        /// Tap event for the sign in list. Opens the selected sign in for editing.
        /// </summary>
        private void LstSignIns_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new SignInPage(vm.Event, e.Item as SignIn));
        }

        /// <summary>
        /// Click event for the add sign in button. Opens a blank sign in screen for the event
        /// </summary>
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage(vm.Event));
        }
    }
}