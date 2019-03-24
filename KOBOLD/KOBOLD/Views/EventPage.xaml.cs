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
	public partial class EventPage : ContentPage
	{
        EventPageViewModel vm;

		public EventPage ()
		{
			InitializeComponent ();
            Title = "New Event";
            vm = new EventPageViewModel();
            this.BindingContext = vm;
		}

        public EventPage(Event @event)
        {
            InitializeComponent();
            Title = "Edit Event Details";
            vm = new EventPageViewModel(@event);
            this.BindingContext = vm;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            vm.SaveEvent();
            Navigation.PopAsync();
        }

        private void BtnComplete_Clicked(object sender, EventArgs e)
        {
            vm.CompleteEvent();
            Navigation.PopAsync();
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}