using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class Echo
    {
        public string Username { get; private set; }
        public IActorRef ActorRef { get; private set; }

        public Echo(string username, IActorRef actorRef)
        {
            ActorRef = actorRef;
            Username = username;
        }
    }
}
