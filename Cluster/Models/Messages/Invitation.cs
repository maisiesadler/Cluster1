using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class Invitation
    {
        public string Key { get; private set; }
        public string Username { get; private set; }

        public Invitation(string key, string username)
        {
            Key = key;
            Username = username;
        }
    }
}
