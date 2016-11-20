using Akka.Configuration;
using System.IO;
using Newtonsoft.Json;
using Models.Models;

namespace Models
{
    public interface IMyClonable<T>
    {
        T Clone();
    }

    public static class Helper
    {
        public static Config GetConfig(string hostPort, string publicHostName, string ip)
        {
            return ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
    }
    remote {
        helios.tcp {
            port = " + hostPort + @"
            hostname = " + ip + @"
            public-hostname = " + publicHostName + @"
        }
    }
}
");
        }

        public static Config GetConfig(ClusterConfig clusterConfig)
        {
            return ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
    }
    remote {
        helios.tcp {
            port = " + clusterConfig.HostPort + @"
            hostname = " + clusterConfig.Ip + @"
            public-hostname = " + clusterConfig.PublicPortName + @"
        }
    }
}
");
        }

        public static ClusterConfig DeserializeConfig(string fileName)
        {
            var file = File.ReadAllText(fileName);
            var config = JsonConvert.DeserializeObject<ClusterConfig>(file);

            return config;
        }

        public static void SerializeConfig(string fileName, ClusterConfig config)
        {
            var json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public static ClusterInvitation TryReadInvitation()
        {
            var fp = "invitation.config";
            if (File.Exists(fp))
            {
                var file = File.ReadAllText(fp);
                var invitation = JsonConvert.DeserializeObject<ClusterInvitation>(file);
                return invitation;
            }
            return null;
        }
    }
}
