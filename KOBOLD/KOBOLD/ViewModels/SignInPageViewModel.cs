using KOBOLD.Data;
using KOBOLD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KOBOLD.ViewModels
{
    class SignInPageViewModel : BaseViewModel
    {
        private SignIn signIn;
        public SignIn SignIn
        {
            get
            {
                return signIn;
            }
            set
            {
                if (signIn != value)
                {
                    signIn = value;

                    NotifyPropertyChanged("SignIn");
                }
            }
        }

        private Event @event;
        public Event Event
        {
            get
            {
                return @event;
            }
            set
            {
                if (@event != value)
                {
                    @event = value;

                    NotifyPropertyChanged("Event");
                }
            }
        }

        private bool edit;
        public bool Edit
        {
            get
            {
                return edit;
            }
            set
            {
                if (edit != value)
                {
                    edit = value;

                    NotifyPropertyChanged("Edit");
                }
            }
        }

        private ObservableCollection<string> signInDays;
        public ObservableCollection<string> SignInDays
        {
            get
            {
                return signInDays;
            }
            set
            {
                signInDays = value;

                NotifyPropertyChanged("SignInDays");
            }
        }

        public SignInPageViewModel(Event @event)
        {
            Event = @event;

            SignInDays = new ObservableCollection<string>
            {                
                "Friday"
                , "Saturday"
                , "Sunday"
                , "Monday"
                , "Tuesday"
                , "Wednesday"
                , "Thursday"
            };

            SignIn = new SignIn
            {
                EventId = @event.EventId.Value
                , SignInDay = DateTime.Now.DayOfWeek.ToString()
                , Kingdom = Event.DefaultKingdom ?? string.Empty
                , Park = Event.DefaultPark ?? string.Empty
            };
        }

        public SignInPageViewModel(Event @event, SignIn signIn)
        {
            Event = @event;

            SignInDays = new ObservableCollection<string>
            {
                "Friday"
                , "Saturday"
                , "Sunday"
                , "Monday"
                , "Tuesday"
                , "Wednesday"
                , "Thursday"
            };

            SignIn = signIn;

            Edit = true;
        }

        /// <summary>
        /// Adds or updates the sign in
        /// </summary>
        /// <returns>True if successful</returns>
        public bool SaveSignIn()
        {
            if (string.IsNullOrEmpty(SignIn.MundaneName) || string.IsNullOrEmpty(SignIn.PersonaName) || string.IsNullOrEmpty(SignIn.SelectedClass) || string.IsNullOrEmpty(SignIn.Park) || string.IsNullOrEmpty(SignIn.Kingdom))
            {
                App.Current.MainPage.DisplayAlert("Error", "Mundane Name, Persona Name, Class, Park, and Kingdom are required.", "OK");
                return false;
            }

            if (SignIn.SignInId.HasValue)
            {                
                return LocalDB.UpdateSignIn(SignIn);
            }
            else
            {
                var signIns = LocalDB.GetSignIns(Event.EventId.Value);
                SignIn.LineNumber = signIns.Any() ? signIns.Last().LineNumber + 1 : 1;
                return LocalDB.AddSignIn(SignIn);
            }
        }

        /// <summary>
        /// Deletes the sign in
        /// </summary>
        /// <returns>True if successful</returns>
        public bool DeleteSignIn()
        {
            return LocalDB.DeleteSignIn(SignIn);
        }
    }
}
