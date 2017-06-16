using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantApp.Data
{
    public class MyEvent : Entity
    {
        private string title;
        private string place;
        private string description;
        private DateTime startDateTime;
        private TimeSpan endTime;

        public string Title
        {
            get {return title; }
            set { title = value; }
        }
        public string Place
        {
            get { return place; }
            set { place = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value; }
        }
        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public string StartDateTimeText
        {
            get{ return startDateTime.ToString("dd.MM.yyyy HH:mm"); }
        }

        public string EndTimeText
        {
            get {return endTime.ToString(@"hh\:mm");}
        }

    }
}
