using Models;
using Models.Implementation;
using System;

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
            Console.WriteLine("Type username");
            var un = Console.ReadLine();

            using (var echoImpl = new ConsoleEchoImpl(conf, un))
            {
                var invitation = Helper.TryReadInvitation();
                if (invitation != null)
                {
                    echoImpl.UseInvitation(invitation);
                }
                else
                {
                    Console.WriteLine("Invitation missing or invalid.");
                    echoImpl.TryCreateNewCluster();
                }
            }
        }
    }
}
