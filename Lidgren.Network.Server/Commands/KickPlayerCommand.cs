using Lidgren.Network;
using LidgrenServer.Managers;
using NetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer.Commands
{
    class KickPlayerCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", string.Format("Kicking {0}", playerAndConnection.Player.Username));
            var outmessage = server.NetServer.CreateMessage();
            outmessage.Write((byte)PacketType.Kick);
            outmessage.Write(playerAndConnection.Player.Username);
            server.NetServer.SendToAll(outmessage, NetDeliveryMethod.ReliableOrdered);
            //Kick player
            playerAndConnection.Connection.Disconnect("Bye bye, you're kicked.");
        }
    }
}
