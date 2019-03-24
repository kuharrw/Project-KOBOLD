using KOBOLD.Data;
using KOBOLD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KOBOLD.ViewModels
{
    class SignInSheetPageViewModel : BaseViewModel
    {
        private ObservableCollection<SignIn> signIns;
        public ObservableCollection<SignIn> SignIns
        {
            get
            {
                return signIns;
            }
            set
            {
                if(signIns != value)
                {
                    signIns = value;

                    NotifyPropertyChanged("SignIns");
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

        public SignInSheetPageViewModel(Event @event)
        {
            Event = @event;
        }

        /// <summary>
        /// Refreshes the list of sign ins for the event
        /// </summary>
        public void Refresh()
        {
            SignIns = LocalDB.GetSignIns(Event.EventId.Value);

            //new ObservableCollection<SignIn>
            //{
            //    new SignIn
            //    {
            //        EventId = Event.EventId.Value
            //        , MundaneName = "Ryan Kuhar"
            //        , PersonaName = "Raten Firewalker"
            //        , SelectedClass = "Assassin"
            //        , Park = "Emerald Glades"
            //        , Kingdom = "Rising Winds"
            //    }
            //    , new SignIn
            //    {
            //        EventId = Event.EventId.Value
            //        , MundaneName = "Zach Berno"
            //        , PersonaName = "Flail Snail"
            //        , SelectedClass = "Monster"
            //        , Park = "Emerald Glades"
            //        , Kingdom = "Rising Winds"
            //    }
            //};
        }
    }
}
