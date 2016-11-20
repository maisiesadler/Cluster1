using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class CreateClusterMessage
    {
        public string ClusterName { get; set; }
        public CreateClusterMessage(string clusterName)
        {
            ClusterName = clusterName;
        }
    }
}
