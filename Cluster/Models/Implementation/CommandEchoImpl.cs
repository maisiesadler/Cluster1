using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Models.Implementation
{
    public abstract class CommandEchoImpl : EchoImpl
    {
        public CommandEchoImpl(ClusterConfig systemConfig, string username, Action<string> writeMessage, object p = null)
            : base(systemConfig, username, writeMessage, p)
        {
            _commands.Add("/ci", new Commands { Action = CreateInvitation, Description = "Creates a valid invitation for another user to join session" });
            _commands.Add("/inv", new Commands { Action = ShowInvitation, Description = "Shows current saved invitation" });
            _commands.Add("/copyinvitation", new Commands { Action = CopyInvToClipboard, Description = "Copies the current invitation to clipboard" });
            _commands.Add("/setinvitation", new Commands { Action = SetInvitationFromClipboard, Description = "Sets current invitation from the clipboard" });
        }

        private Dictionary<string, Commands> _commands = new Dictionary<string, Commands>();

        protected bool ExecuteCommand(string command)
        {
            if (_commands.ContainsKey(command))
            {
                _commands[command].Action.Invoke();
                return true;
            }

            return false;
        }

        public IEnumerable<Tuple<string, string>> GetListOfCommands()
        {
            foreach (var key in _commands.Keys)
            {
                yield return new Tuple<string, string>(key, _commands[key].Description);
            }
        }

        #region Commands
        protected ClusterInvitation GetInvitation()
        {
            var invitation = Helper.TryReadInvitation();
            return invitation;
        }
        protected abstract void ShowInvitation();

        protected void CopyInvToClipboard()
        {
            var jsonInv = JsonConvert.SerializeObject(GetInvitation());
            Thread thread = new Thread(() => Clipboard.SetText(jsonInv));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
        }
        protected void SetInvitationFromClipboard()
        {
            var inv = Clipboard.GetText();
            Helper.SetInvitation(inv);
        }
        #endregion        
    }

    public class Commands
    {
        public string Description { get; set; }
        public Action Action { get; set; }
    }

    public static class CommandHelper
    {
        public static Commands AddDescription(this Action action, string description)
        {
            return new Commands { Action = action, Description = description };
        }
    }
}
