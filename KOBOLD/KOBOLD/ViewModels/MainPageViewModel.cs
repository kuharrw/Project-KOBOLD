using KOBOLD.Data;
using KOBOLD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KOBOLD.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Event> events;
        //List of open events
        public ObservableCollection<Event> Events
        {
            get
            {
                return events;
            }
            set
            {
                if(events != value)
                {
                    events = value;

                    NotifyPropertyChanged("Events");
                }
            }
        }

        public MainPageViewModel()
        {
        }

        /// <summary>
        /// Refreshes the event list from the local database
        /// </summary>
        public void Refresh()
        {
            Events = LocalDB.GetEvents();
            //new ObservableCollection<Event>
            //{
            //    new Event{Name = "Test event 1", EventId = 0, EventDate = DateTime.Now}
            //    , new Event{Name = "Test event 2", EventId = 0, EventDate = DateTime.Now}
            //    , new Event{Name = "Test event 3", EventId = 0, EventDate = DateTime.Now}
            //}; 
        }
    }
}
