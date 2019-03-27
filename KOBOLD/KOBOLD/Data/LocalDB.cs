using KOBOLD.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace KOBOLD.Data
{
    /// <summary>Contains access point and creation of the local sqlite database</summary>
    public static class LocalDB
    {
        //Constant string for the path to the DB
        public static readonly string DbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KOBOLD.db");
        //Sqlite connection object
        private static SQLiteConnection db = null;

        /// <summary>Initializes the DB. Should be called when the application loads</summary>
        /// <returns>bool indication of success</returns>
        public static bool InitializeDatabaseConnection()
        {
            try
            {
                db = new SQLiteConnection(DbFilePath);
                db.CreateTable<Event>();
                db.CreateTable<SignIn>();

                return true;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return false;
            }
        }

        #region Events
        /// <summary>Adds a new event</summary>
        /// <param name="event">The new event to be added</param>
        /// <returns>True if successful</returns>
        public static bool AddEvent(Event @event)
        {
            try
            {
                db.Insert(@event);

                return @event.EventId.Value > 0;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return false;
            }
        }

        /// <summary>Gets a single event</summary>
        /// <param name="eventId">The ID of the event requested</param>
        /// <returns>The event requested</returns>
        public static Event GetEvent(int eventId)
        {
            try
            {
                return db.Get<Event>(eventId);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return null;
            }
        }

        /// <summary>Gets a collection of all events</summary>
        /// <param name="includeCompleted">Whether or not to include completed events in the list. Defaults to false, meaning it will only get active events</param>
        /// <returns>The list of events requested</returns>
        public static ObservableCollection<Event> GetEvents(bool includeCompleted = false)
        {
            try
            {
                var events = db.Table<Event>().Where(e => !e.Complete || includeCompleted);

                return new ObservableCollection<Event>(events);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return null;
            }
        }

        /// <summary>Updates an event</summary>
        /// <param name="event">The event to update, based on the primary key</param>
        /// <returns>True if the update is successful. Call GetEvent afterwards to get the updated event</returns>
        public static bool UpdateEvent(Event @event)
        {
            try
            {
                return db.Update(@event) > 0;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return false;
            }
        }
        #endregion

        #region SignIns
        /// <summary>Adds a new sign in</summary>
        /// <param name="signIn">The new sign in to be added</param>
        /// <returns>True if successful</returns>
        public static bool AddSignIn(SignIn signIn)
        {
            try
            {
                db.Insert(signIn);

                return signIn.SignInId.Value > 0;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return false;
            }
        }

        /// <summary>Gets a single sign in</summary>
        /// <param name="signInId">The ID of the sign in requested</param>
        /// <returns>The sign in requested</returns>
        public static SignIn GetSignIn(int signInId)
        {
            try
            {
                return db.Get<SignIn>(signInId);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return null;
            }
        }

        /// <summary>Gets a collection of all sign ins for a single event</summary>
        /// <param name="eventId">The event of the sign ins requested</param>
        /// <returns>The list of sign ins requested</returns>
        public static ObservableCollection<SignIn> GetSignIns(int eventId)
        {
            try
            {
                var signIns = db.Table<SignIn>().Where(s => s.EventId == eventId);

                return new ObservableCollection<SignIn>(signIns);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return null;
            }
        }

        /// <summary>Updates an sign in</summary>
        /// <param name="signIn">The sign in to update, based on the primary key</param>
        /// <returns>True if the update is successful. Call GetSignIn afterwards to get the updated event</returns>
        public static bool UpdateSignIn(SignIn signIn)
        {
            try
            {
                return db.Update(signIn) > 0;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return false;
            }
        }

        /// <summary>
        /// Delete a sign in
        /// </summary>
        /// <param name="signIn">The sign in to delete</param>
        /// <returns></returns>
        public static bool DeleteSignIn(SignIn signIn)
        {
            try
            {
                return db.Delete(signIn) > 0;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured accessing the local database", "OK");
                return false;
            }
        }
        #endregion
    }

}
