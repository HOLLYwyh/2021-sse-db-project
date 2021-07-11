using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Message
    {
        public string Messageid { get; set; }
        public string Chatroomid { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Chatroom Chatroom { get; set; }
    }
}
