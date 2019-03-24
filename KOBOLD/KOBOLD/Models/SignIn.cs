using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KOBOLD.Models
{    
    [Table("sign_in")]
    ///<summary>Model for sign ins. One sign in is for a single player for a single event. Some fields may not apply, for example wristband number for a regular park day.</summary>
    public class SignIn
    {
        private int? signInId;
        private int eventId;
        private string mundaneName;
        private string personaName;
        private string selectedClass;
        private string park;
        private string kingdom;
        private bool newPlayer;
        private string wristbandNumber;
        private string parkingPassNumber;
        private string guardianName;
        private bool lookThePart;
        private string customFieldOne;
        private string customFieldTwo;
        private string customFieldThree;


        [PrimaryKey, AutoIncrement]
        public int? SignInId { get => signInId; set => signInId = value; }        
        public int EventId { get => eventId; set => eventId = value; }
        public string MundaneName { get => mundaneName; set => mundaneName = value; }
        public string PersonaName { get => personaName; set => personaName = value; }
        public string SelectedClass { get => selectedClass; set => selectedClass = value; }
        public string Park { get => park; set => park = value; }
        public string Kingdom { get => kingdom; set => kingdom = value; }
        public bool NewPlayer { get => newPlayer; set => newPlayer = value; }
        public string WristbandNumber { get => wristbandNumber; set => wristbandNumber = value; }
        public string ParkingPassNumber { get => parkingPassNumber; set => parkingPassNumber = value; }
        public string GuardianName { get => guardianName; set => guardianName = value; }
        public bool LookThePart { get => lookThePart; set => lookThePart = value; }
        public string CustomFieldOne { get => customFieldOne; set => customFieldOne = value; }
        public string CustomFieldTwo { get => customFieldTwo; set => customFieldTwo = value; }
        public string CustomFieldThree { get => customFieldThree; set => customFieldThree = value; }
    }
}
