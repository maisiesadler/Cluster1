using Akka.Actor;
using Models.Messages;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Actors
{
    public class ConsoleEcho : BaseEchoActor
    {
        public ConsoleEcho(string username) : base(username)
        {
        }

        protected override void Debug(string msg)
        {
            Console.WriteLine("DEBUG:" + msg);
        }

        protected override void InvitationReceivedMsg(Invitation invitation, bool decision)
        {
            Console.WriteLine("Received invitation from " + invitation.Username + ", " + (decision ? "accepted" : "rejected"));
        }

        protected override void MyInvitationRejected()
        {
            Console.WriteLine("Invitation rejected");
        }

        protected override void WriteMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
