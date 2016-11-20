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

namespace Cluster3
{
    class Program
    {
        static void Main(string[] args)
        {
            //var config = Helper.GetConfig("8083", "192.168.0.59");

            //Helper.SerializeConfig("cluster.config", new ClusterConfig { HostPort = "8083", Ip = "192.168.0.59" });
            var conf = Helper.DeserializeConfig("cluster.config");
            var config = Helper.GetConfig(conf);


            Console.WriteLine("Type username");
            var un = Console.ReadLine();

            //var un = "maisie";
            using (var system = ActorSystem.Create("UserThree", config))
            {
                //var echo = system.ActorOf(Props.Create(() => new EchoActor(un)), "Echo" + un);
                //Console.ReadKey();

                //Invitation i = new Invitation("123", un);
                //var str2 = "akka.tcp://UserTwo@192.168.0.59:8082/user/Echoms";
                ////var addUser1 = port2 == "8081" ? "" : "ms";
                //echo.Tell(new SignIn(i, str2));

                //while (true)
                //{
                //    var msg = Console.ReadLine();
                //    echo.Tell(new EncMessage(msg));
                //}
            }
        }
    }
}
