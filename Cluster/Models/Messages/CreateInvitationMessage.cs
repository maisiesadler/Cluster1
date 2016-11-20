using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class CreateInvitationMessage
    {
        public string Port { get; private set; }
        public string PublicPortName { get; private set; }

        public CreateInvitationMessage(ClusterConfig clusterConfig)
        {
            Port = clusterConfig.HostPort;
            PublicPortName = clusterConfig.PublicPortName;
        }
    }
}
