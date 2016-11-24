using Models.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Akka.Actor;
using Models.Models;

namespace ConsoleGUI
{
    public class GUIEchoImpl : CommandEchoImpl
    {
        GuiEchoActions _actions;
        bool signedIn;
        public GUIEchoImpl(ClusterConfig systemConfig, string username, Action<string> writeMessage, GuiEchoActions actions)
            : base(systemConfig, username, writeMessage, actions)
        {
            _actions = actions;
        }

        protected override void CodeToRunBeforeCreatingActor(object p)
        {
            if (p is GuiEchoActions)
            {
                var a = p as GuiEchoActions;
                a.MyInvitationAccepted += InvitationAccepted;
                _p = p;
            }
        }

        public void InvitationAccepted()
        {
            signedIn = true;
        }

        public void TryUseInvitation(ClusterInvitation invitation)
        {
            if (!signedIn)
            {
                JoinUsingInvitation(invitation);
            }
        }

        public override void Begin()
        {

        }

        public void Send(string msg)
        {
            if (IsCommand(msg))
            {
                DoCommand(msg);
            }
            else
            {
                SendMessage(msg);
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

        protected override void CreateEcho()
        {
            if (_p != null)
            {
                var actions = _p as GuiEchoActions;
                _echo = _system.ActorOf(Props.Create(() => new GUIEcho(_username, _writeMessage, actions)), "Echo");
            }
        }

        protected override void ShowInvitation()
        {
            var inv = Helper.TryReadInvitation();
            _actions.ShowInvitation(inv);
        }
    }
}