using KOBOLD.Data;
using KOBOLD.Models;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

        internal void Export(Event @event, string v)
        {
            try
            {
                var signIns = LocalDB.GetSignIns(@event.EventId.Value);

                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{@event.Name}_Sign_Ins.csv"));

                using (var textWriter = File.CreateText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{@event.Name}_Sign_Ins.csv")))
                {
                    textWriter.WriteLine($"{@event.Name},{@event.EventDate.Date.ToShortDateString()},{@event.Credits} {(@event.Credits > 1 ? "Credits" : "Credit")}, Total Sign Ins: {signIns.Count}, Paying Sign Ins: {signIns.Where(s => !s.PayExempt).Count()}");
                    textWriter.WriteLine($"Sign In Number, Sign In ID,Event ID,Mundane Name,Persona Name,Class,Park,Kingdom,New Player,Wristband Number,Parking Pass Number,Guardian Name,LTP, Sign In Day, Pay Exempt,{@event.CustomFieldOne},{@event.CustomFieldTwo},{@event.CustomFieldThree}");
                    foreach (var line in DataHelpers.ToCsv(signIns))
                    {
                        textWriter.WriteLine(line);
                    }

                    textWriter.Close();
                }

                var emailTask = CrossMessaging.Current.EmailMessenger;
                if (emailTask.CanSendEmail)
                {
                    var email = new EmailMessageBuilder()
                      .Subject($"{@event.Name} sign ins")
                      .Body("")
                      .WithAttachment(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{@event.Name}_Sign_Ins.csv"), "text/csv")
                      .Build();

                    emailTask.SendEmail(email);
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "Unable to export sign ins", "OK");
                throw;
            }
        }
    }
}
