using Akka.Actor;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class SignedIn
    {
        public CurrentCluster Cluster { get; private set; }
        public SignedIn(CurrentCluster cluster)
        {
            Cluster = cluster;
        }
    }
}
