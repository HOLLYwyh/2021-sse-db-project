using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Chatroom
    {
        public Chatroom()
        {
            Chatusers = new HashSet<Chatuser>();
            Messages = new HashSet<Message>();
        }

        public string Chatroomid { get; set; }
        public decimal Type { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Chatuser> Chatusers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
