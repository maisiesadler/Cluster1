using Akka.Actor;
using Models.Actors;
using Models.Models;
using System;

namespace Models.Implementation
{
    public class ConsoleEchoImpl : CommandEchoImpl
    {
        public ConsoleEchoImpl(ClusterConfig config, string username, Action<string> writeMessage)
            : base(config, username, writeMessage)
        {
        }

        protected override void CreateEcho()
        {
            _echo = _system.ActorOf(Props.Create(() => new ConsoleEcho(_username, _writeMessage)), "Echo");
        }

        public override void Begin()
        {
            while (true)
            {
                var msg = Console.ReadLine();
                if (IsCommand(msg))
                {
                    DoCommand(msg);
                }
                else
                {
                    SendMessage(msg);
                }
            }
        }

        public bool IsCommand(string msg)
        {
            return msg.StartsWith("/", StringComparison.Ordinal);
        }

        public void DoCommand(string msg)
        {
            if (!ExecuteCommand(msg))
            {
                Console.WriteLine("Command not found");
            }
        }

        public void UseInvitation(ClusterInvitation invitation)
        {
            Console.WriteLine("Use invitation? (Y/N)");
            var k = Console.ReadKey();
            if (k.Key == ConsoleKey.Y)
            {
                JoinUsingInvitation(invitation);
                Begin();
            }
            else
            {
                TryCreateNewCluster();
            }
        }

        public void TryCreateNewCluster()
        {
            Console.WriteLine("Create new cluster? (Y/N)");
            var k = Console.ReadKey();
            if (k.Key == ConsoleKey.Y)
            {
                CreateNewCluster();
                Begin();
            }
        }

        protected override void ShowInvitation()
        {
            Console.WriteLine(GetInvitation());
        }

        protected override void CodeToRunBeforeCreatingActor(object p)
        {
            
        }
    }
}
