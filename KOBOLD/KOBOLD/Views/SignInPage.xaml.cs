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
	public partial class SignInPage : ContentPage
	{
        SignInPageViewModel vm;

        public SignInPage()
        {
            InitializeComponent();
        }

		public SignInPage (Event @event)
        {
            InitializeComponent();
            Title = "New Sign In";
            vm = new SignInPageViewModel(@event);
            this.BindingContext = vm;
            SetCustomFieldVisibility(@event);
        }

        public SignInPage(Event @event, SignIn @signIn)
        {
            InitializeComponent();
            Title = $@"{signIn.PersonaName}'s sign in for {@event.Name}";
            vm = new SignInPageViewModel(@event, signIn);
            this.BindingContext = vm;
            SetCustomFieldVisibility(@event);
        }

        /// <summary>
        /// Sets the visibility of the custom fields based on whether or not there is a custom field on the event
        /// </summary>
        /// <param name="event">The event the sign in is for</param>
        private void SetCustomFieldVisibility(Event @event)
        {
            if (string.IsNullOrEmpty(@event.CustomFieldOne))
            {
                grdCustomFieldOne.IsVisible = false;
            }

            if (string.IsNullOrEmpty(@event.CustomFieldTwo))
            {
                grdCustomFieldTwo.IsVisible = false;
            }

            if (string.IsNullOrEmpty(@event.CustomFieldThree))
            {
                grdCustomFieldThree.IsVisible = false;
            }
        }

        /// <summary>
        /// Click event for saving the sign in. Exits the screen after saving
        /// </summary>
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if(vm.SaveSignIn())
                Navigation.PopAsync();
        }

        /// <summary>
        /// Click event for the delete button. Exits the screen after deleting
        /// </summary>
        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            vm.DeleteSignIn();
            Navigation.PopAsync();
        }

        /// <summary>
        /// Cancel button click event. Exits the screen
        /// </summary>
        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}