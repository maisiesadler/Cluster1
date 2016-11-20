using Akka.Actor;
using Akka.Configuration;
using Models;
using Models.Actors;
using Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster
{
    class Program
    {
        static void Main(string[] args)
        {
            //ClusterConfig cc = new ClusterConfig
            //{
            //    Ip = "0.0.0.0",
            //    HostPort = "8082",
            //    //InvitationAddress = "akka.tcp://UserOne@localhost:8081/user/Echo",
            //    PublicPortName = "localhost",
            //    //InvitationKey = "123"
            //};
            //Helper.SerializeConfig("cluster.config", cc);

            var conf = Helper.DeserializeConfig("cluster.config");
            var config = Helper.GetConfig(conf);

            var un = "echo";
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
}
