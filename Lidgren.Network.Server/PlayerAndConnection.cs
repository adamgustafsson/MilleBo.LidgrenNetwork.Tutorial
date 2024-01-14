using Lidgren.Network;
using NetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer
{
    internal class PlayerAndConnection
    {
        public Player Player { get; set; }
        public NetConnection Connection { get; set; }

        public PlayerAndConnection(Player player, NetConnection connection)
        {
            Player = player;
            Connection = connection;
        }
    }
}
