using Models;
using Models.Implementation;
using System;

namespace Cluster2
{
    class Program
    {
        static void Main(string[] args)
        {
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
