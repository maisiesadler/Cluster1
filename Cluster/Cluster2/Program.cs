using Akka.Actor;
using Models;
using Models.Actors;
using Models.Messages;
using Models.Models;
using System;

namespace Cluster2
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = Helper.DeserializeConfig("cluster.config");
            var config = Helper.GetConfig(conf);

            Console.WriteLine("Type username");
            var un = Console.ReadLine();

            using (var system = ActorSystem.Create("UserOne", config))
            {
                var echo = system.ActorOf(Props.Create(() => new ConsoleEcho(un)), "Echo");
                Console.ReadKey();

                var invitation = Helper.TryReadInvitation();
                if (invitation != null)
                {
                    var i = new Invitation(invitation.InvitationKey, un);
                    echo.Tell(new SignIn(i, invitation.InvitationAddress));

                    while (true)
                    {
                        var msg = Console.ReadLine();
                        echo.Tell(new EncMessage(msg));
                    }
                }
                else
                {
                    Console.WriteLine("Invitation missing or invalid, create new cluster? (Y/N)");

                    var k = Console.ReadKey();
                    if (k.Key == ConsoleKey.Y)
                    {
                        echo.Tell(new CreateClusterMessage("Big fat cluster"));
                        echo.Tell(new CreateInvitationMessage(conf));

                        while (true)
                        {
                            var msg = Console.ReadLine();
                            echo.Tell(new EncMessage(msg));
                        }
                    }
                }
            }
        }
    }

    public class ConsoleEchoImpl
    {
        IActorRef _echo;
        bool _listening;

        public ConsoleEchoImpl(IActorRef echo)
        {
            _echo = echo;
        }

        public void JoinUsingInvitation(ClusterInvitation invitation)
        {
            var i = new Invitation(invitation.InvitationKey, "username");
            _echo.Tell(new SignIn(i, invitation.InvitationAddress));
        }

        public void Begin()
        {
            while (_listening)
            {
                var msg = Console.ReadLine();
                _echo.Tell(new EncMessage(msg));
            }
        }

        public void StopListening()
        {
            _listening = false;
        }
    }
}
