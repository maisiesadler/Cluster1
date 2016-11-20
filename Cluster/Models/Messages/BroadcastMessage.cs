using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class BroadcastMessage
    {
        public string Sender { get; private set; }
        public EncMessage Message { get; private set; }

        public BroadcastMessage(EncMessage message, string sender)
        {
            Message = message;
            Sender = sender;
        }
    }
}
