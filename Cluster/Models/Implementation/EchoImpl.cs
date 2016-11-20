using Akka.Actor;
using Akka.Configuration;
using Models.Messages;
using Models.Models;
using System;

namespace Models.Implementation
{
    public abstract class EchoImpl : IDisposable
    {
        protected IActorRef _echo;
        protected ActorSystem _system;
        protected readonly string _username;
        protected ClusterConfig _clusterConfig;

        protected EchoImpl(ClusterConfig clusterConfig, string username)
        {
            _username = username;
            _clusterConfig = clusterConfig;
            var config = Helper.GetConfig(clusterConfig);
            CreateActorSystemUsingConfig(config);
        }

        protected void CreateActorSystemUsingConfig(Config systemConfig)
        {
            _system = ActorSystem.Create("UserOne", systemConfig);
            CreateEcho();
        }

        protected abstract void CreateEcho();

        protected void JoinUsingInvitation(ClusterInvitation invitation)
        {
            var i = new Invitation(invitation.InvitationKey, _username);
            _echo.Tell(new SignIn(i, invitation.InvitationAddress));
        }

        protected void CreateNewCluster()
        {
            _echo.Tell(new CreateClusterMessage("Big fat cluster"));
            CreateInvitation();
        }
        
        protected void CreateInvitation()
        {
            _echo.Tell(new CreateInvitationMessage(_clusterConfig));
        }

        public void SendMessage(string msg)
        {
            _echo.Tell(new EncMessage(msg));
        }

        protected abstract string GetMessage();

        public void Dispose()
        {
            _system.Dispose();
        }
    }
}
