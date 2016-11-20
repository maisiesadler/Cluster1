using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ClusterInvitation
    {
        public string InvitationAddress { get; set; }
        public string InvitationKey { get; set; }

        public override string ToString()
        {
            return $"Address: {InvitationAddress}. Key: {InvitationKey}.";
        }
    }
}
