using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class SignIn
    {
        public Invitation Invitation { get; private set; }
        public string Path { get; private set; }

        public SignIn(Invitation invitation, string path)
        {
            Invitation = invitation;
            Path = path;
        }
    }
}
