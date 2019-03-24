using KOBOLD.Data;
using KOBOLD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KOBOLD.ViewModels
{
    class EventPageViewModel : BaseViewModel
    {
        private Event @event;
        public Event Event
        {
            get
            {
                return @event;
            }
            set
            {
                if(@event != value)
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
                if(edit != value)
                {
                    edit = value;

                    NotifyPropertyChanged("Edit");
                }
            }
        }

        /// <summary>
        /// Constructor for adding a new event
        /// </summary>
        public EventPageViewModel()
        {
            Event = new Event
            {
                EventDate = DateTime.Now.Date
                , Credits = 1
            };
        }

        /// <summary>
        /// Constructor for opening an event
        /// </summary>
        /// <param name="event">The Event to open</param>
        public EventPageViewModel(Event @event)
        {
            Event = @event;

            Edit = true;
        }

        /// <summary>
        /// Adds or updates an event's info
        /// </summary>
        /// <returns>True if successful</returns>
        public bool SaveEvent()
        {
            //update
            if (Event.EventId.HasValue)
            {
               return LocalDB.UpdateEvent(Event);
            }
            else
            {
                return LocalDB.AddEvent(Event) > 0;
            }
        }

        /// <summary>
        /// Sets the event to complete, then saves it
        /// </summary>
        /// <returns>True if successful</returns>
        public bool CompleteEvent()
        {
            Event.Complete = true;

            return SaveEvent();
        }
    }
}
