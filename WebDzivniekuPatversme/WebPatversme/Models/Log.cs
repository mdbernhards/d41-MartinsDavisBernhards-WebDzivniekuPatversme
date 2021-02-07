using System;

namespace WebDzivniekuPatversme.Models
{
    public class Log
    {
        public string LogID { set; get; }

        public string ItemID { set; get; }

        public string UserID { set; get; }

        public int ItemType { set; get; }

        public int MessageType { set; get; }

        public DateTime TimeStamp { set; get; }
    }
}