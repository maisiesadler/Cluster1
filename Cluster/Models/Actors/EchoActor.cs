//using Akka.Actor;
//using Models.Messages;
//using Models.Models;
//using System;

//namespace Models.Actors
//{
//    public class EchoActor : ReceiveActor
//    {
//        private CurrentCluster currentCluster;
//        private Echo thisUser;
//        public EchoActor(string username)
//        {
//            thisUser = new Echo(username, Self);
//            Ready();
//        }

//        private void SignedIn()
//        {
//            Console.WriteLine("SignedIn");
//            Receive<EncMessage>(m =>
//            {
//                foreach (var actor in currentCluster.Users)
//                {
//                    actor.Value.Tell(new BroadcastMessage(m, thisUser.Username));
//                }
//            });
//            Receive<BroadcastMessage>(bm =>
//            {
//                Console.WriteLine($"{bm.Sender}: {bm.Message.Msg}");
//            });
//            Receive<Invitation>(i =>
//            {
//                if (currentCluster.ValidateInvitation(i))
//                {
//                    var echo = new Echo(i.Username, Sender);
//                    currentCluster.AddUser(echo);

//                    Console.WriteLine("Recieved invitation from " + i.Username);

//                    foreach (var actor in currentCluster.Users)
//                    {
//                        if (actor.Value != Self)
//                        {
//                            actor.Value.Tell(echo);
//                        }
//                    }

//                    Sender.Tell(new SignedIn(currentCluster.Clone()));
//                }
//                else
//                {
//                    Console.WriteLine("Received invalid invitation from " + i.Username);
//                    Sender.Tell(new RejectInvitationMessage());
//                }
//            });
//            Receive<Echo>(e =>
//            {
//                currentCluster.AddUser(e);
//            });
//            Receive<CreateInvitationMessage>(cim =>
//            {
//                var sender = Context;
//                ModelHelper.CreateInvitation(currentCluster.Clone(), Self, cim);
//            });
//        }

//        protected override void Unhandled(object message)
//        {
//            base.Unhandled(message);
//        }

//        private void Ready()
//        {
//            Receive<SignIn>(si =>
//            {
//                var path = Context.ActorSelection(si.Path);
//                path.Tell(si.Invitation);
//            });
//            Receive<SignedIn>(si =>
//            {
//                Console.WriteLine("Got signed in message");
//                currentCluster = si.Cluster;
//                Become(SignedIn);
//            });
//            Receive<RejectInvitationMessage>(si =>
//            {
//                Console.WriteLine("Invitation rejected");
//            });
//            Receive<CreateClusterMessage>(ccm =>
//            {
//                Console.WriteLine(Self);
//                currentCluster = new CurrentCluster(ccm.ClusterName, new Guid().ToString());
//                currentCluster.AddUser(thisUser);
//                Become(SignedIn);
//            });
//        }
//    }
//}
