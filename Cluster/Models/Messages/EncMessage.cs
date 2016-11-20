using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Messages
{
    public class EncMessage
    {
        public string Msg { get; private set; }
        public EncMessage(string msg)
        {
            Msg = msg;
        }
    }
}
