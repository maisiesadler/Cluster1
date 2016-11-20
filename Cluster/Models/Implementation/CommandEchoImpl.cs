using Models.Models;
using System;
using System.Collections.Generic;

namespace Models.Implementation
{
    public abstract class CommandEchoImpl : EchoImpl
    {
        public CommandEchoImpl(ClusterConfig systemConfig, string username) : base(systemConfig, username)
        {
            _commands.Add("/ci", CreateInvitation);
            _commands.Add("/inv", ShowInvitation);
        }

        private Dictionary<string, Action> _commands = new Dictionary<string, Action>();

        protected bool ExecuteCommand(string command)
        {
            if (_commands.ContainsKey(command))
            {
                _commands[command].Invoke();
                return true;
            }

            return false;
        }


        #region Commands
        protected ClusterInvitation GetInvitation()
        {
            var invitation = Helper.TryReadInvitation();
            return invitation;
        }
        protected abstract void ShowInvitation();
        #endregion
    }
}
