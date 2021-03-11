using System;

namespace WebDzivniekuPatversme.Models
{
    public class Log
    {
        public string Id { set; get; }

        public string ItemId { set; get; }

        public string UserId { set; get; }

        public int ItemType { set; get; }

        public int MessageType { set; get; }

        public DateTime TimeStamp { set; get; }
    }
}