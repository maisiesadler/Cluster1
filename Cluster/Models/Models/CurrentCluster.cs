using Akka.Actor;
using Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class CurrentCluster : IMyClonable<CurrentCluster>
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        private readonly Dictionary<string, IActorRef> _users;
        public Dictionary<string, IActorRef> Users { get { return _users; } }

        public CurrentCluster(string name)
        {
            CreateKey();
            Name = name;
            _users = new Dictionary<string, IActorRef>();
        }

        internal CurrentCluster(string name, string key, Dictionary<string, IActorRef> users)
        {
            Key = key;
            Name = name;
            _users = users;
        }

        public void AddUser(Echo user)
        {
            _users.Add(user.Username, user.ActorRef);
        }

        public string GetUsername(IActorRef actorRef)
        {
            foreach (var user in _users)
            {
                if (user.Value == actorRef)
                {
                    return user.Key;
                }
            }
            return "Unknown";
        }
        
        private void CreateKey()
        {
            if (string.IsNullOrEmpty(Key))
            {
                Key = Guid.NewGuid().ToString();
            }
            else
            {
                throw new Exception("Key is already created for " + Name);
            }
        }

        public bool ValidateInvitation(Invitation i)
        {
            return i.Key == Key;
        }

        public CurrentCluster Clone()
        {
            return new CurrentCluster(Name, Key, _users);
        }
    }
}
