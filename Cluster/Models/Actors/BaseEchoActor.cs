using Akka.Actor;
using Models.Messages;
using Models.Models;
using System;

namespace Models.Actors
{
    public abstract class BaseEchoActor : ReceiveActor
    {
        private CurrentCluster currentCluster;
        private Echo thisUser;
        protected Action<string> _writeMessage;
        public BaseEchoActor(string username, Action<string> writeMessage)
        {
            _writeMessage = writeMessage;
            thisUser = new Echo(username, Self);
            Ready();
        }

        private void SignedIn()
        {
            Debug("SignedIn");
            Receive<EncMessage>(m =>
            {
                foreach (var actor in currentCluster.Users)
                {
                    actor.Value.Tell(new BroadcastMessage(m, thisUser.Username));
                }
            });
            Receive<BroadcastMessage>(bm =>
            {
                WriteMessage($"{bm.Sender}: {bm.Message.Msg}");
            });
            Receive<Invitation>(i =>
            {
                if (currentCluster.ValidateInvitation(i))
                {
                    var echo = new Echo(i.Username, Sender);
                    currentCluster.AddUser(echo);

                    InvitationReceivedMsg(i, true);

                    foreach (var actor in currentCluster.Users)
                    {
                        if (actor.Value != Self)
                        {
                            actor.Value.Tell(echo);
                        }
                    }

                    Sender.Tell(new SignedIn(currentCluster.Clone()));
                }
                else
                {
                    InvitationReceivedMsg(i, false);
                    Sender.Tell(new RejectInvitationMessage());
                }
            });
            Receive<Echo>(e =>
            {
                currentCluster.AddUser(e);
                Debug("Adding user " + e.Username);
            });
            Receive<CreateInvitationMessage>(cim =>
            {
                ModelHelper.CreateInvitation(currentCluster.Clone(), Self, cim);
                Debug("Invitation created, invitation.config");
            });
            Receive<LogoutMessage>(m =>
            {
                
            });
        }

        protected override void Unhandled(object message)
        {
            base.Unhandled(message);
        }

        private void Ready()
        {
            Receive<SignIn>(si =>
            {
                var path = Context.ActorSelection(si.Path);
                path.Tell(si.Invitation);
            });
            Receive<SignedIn>(si =>
            {
                MyInvitationAccepted();
                currentCluster = si.Cluster;
                Become(SignedIn);
            });
            Receive<RejectInvitationMessage>(si =>
            {
                MyInvitationRejected();
            });
            Receive<CreateClusterMessage>(ccm =>
            {
                Console.WriteLine(Self);
                currentCluster = new CurrentCluster(ccm.ClusterName);
                currentCluster.AddUser(thisUser);
                Become(SignedIn);
            });
        }

        protected abstract void MyInvitationRejected();
        protected abstract void MyInvitationAccepted();
        protected abstract void Debug(string msg);
        protected void WriteMessage(string msg)
        {
            _writeMessage(msg);
        }
        protected abstract void InvitationReceivedMsg(Invitation invitation, bool decision);
    }
}
