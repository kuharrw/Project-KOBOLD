using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KOBOLD.Models
{
    [Table("event")]
    ///<summary>Model for an event. Used to represent either an entire event (coro, midreign, etc) or a single park day</summary>
    public class Event
    {
        private int? eventId;
        private string name;
        private DateTime eventDate;
        private int credits;
        private bool complete;
        private string customFieldOne;
        private string customFieldTwo;
        private string customFieldThree;


        [PrimaryKey, AutoIncrement]
        public int? EventId { get => eventId; set => eventId = value; }
        public string Name { get => name; set => name = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public int Credits { get => credits; set => credits = value; }
        public bool Complete { get => complete; set => complete = value; }
        public string CustomFieldOne { get => customFieldOne; set => customFieldOne = value; }
        public string CustomFieldTwo { get => customFieldTwo; set => customFieldTwo = value; }
        public string CustomFieldThree { get => customFieldThree; set => customFieldThree = value; }
    }
}
