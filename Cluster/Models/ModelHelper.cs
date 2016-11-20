using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Akka.Actor;
using Models.Messages;

namespace Models
{
    internal static class ModelHelper
    {
        internal static void CreateInvitation(CurrentCluster cluster, IActorRef actor, CreateInvitationMessage cim)
        {
            var i = new ClusterInvitation { InvitationAddress = ActorRefToString(actor, cim), InvitationKey = cluster.Key };
            var json = JsonConvert.SerializeObject(i);
            File.WriteAllText("invitation.config", json);
        }

        internal static string ActorRefToString(IActorRef actor, CreateInvitationMessage cim)
        {
            //{[akka://UserOne/user/Echo#748748065]}
            //akka.tcp://UserOne@localhost:8081/user/Echo
            var systemName = actor.Path.Root.ToString().Replace("akka", "akka.tcp");
            var sb = new StringBuilder();
            sb.Append(systemName);
            sb.Length--;

            sb.Append("@" + cim.PublicPortName + ":" + cim.Port);

            sb.Append("/user/" + actor.Path.Name);

            return sb.ToString();
        }
    }
}
