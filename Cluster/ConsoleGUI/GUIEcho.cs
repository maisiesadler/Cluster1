using Models.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Messages;
using Models.Models;

namespace ConsoleGUI
{
    class GUIEcho : BaseEchoActor
    {
        GuiEchoActions _actions;
        public GUIEcho(string username, Action<string> writeMessage, GuiEchoActions actions) : base(username, writeMessage)
        {
            _actions = actions;
        }

        protected override void Debug(string msg)
        {
            _actions.Debug(msg);
        }

        protected override void InvitationReceivedMsg(Invitation invitation, bool decision)
        {
            _actions.InvitationReceivedMsg(invitation, decision);
        }

        protected override void MyInvitationAccepted()
        {
            _actions.MyInvitationAccepted();
        }

        protected override void MyInvitationRejected()
        {
            _actions.MyInvitationRejected();
        }
    }

    public class GuiEchoActions
    {
        public Action<string> Debug;
        public Action<Invitation, bool> InvitationReceivedMsg;
        public Action MyInvitationAccepted;
        public Action MyInvitationRejected;
        public Action<ClusterInvitation> ShowInvitation;
    }
}
