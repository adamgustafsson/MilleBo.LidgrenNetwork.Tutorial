using LidgrenServer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer.MyEventArgs
{
    internal class NewPlayerEventArgs : EventArgs
    {
        public string Username { get; set; }

        public NewPlayerEventArgs(string username)
        {
            Username = username;
        }
    }
}
