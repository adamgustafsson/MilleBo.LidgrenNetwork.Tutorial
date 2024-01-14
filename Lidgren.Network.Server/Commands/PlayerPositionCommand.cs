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
    class PlayerPositionCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            if (playerAndConnection != null)
            {
                managerLogger.AddLogMessage("server", "Sending out new player position to all in group " + gameRoom.GameRoomId);
                var outmessage = server.NetServer.CreateMessage();
                outmessage.Write((byte)PacketType.PlayerPosition);
                outmessage.Write(playerAndConnection.Player.Username);
                outmessage.WriteAllProperties(playerAndConnection.Player.Position);
                server.NetServer.SendMessage(outmessage, gameRoom.Players.Select(p => p.Connection).ToList(),
                    NetDeliveryMethod.ReliableOrdered, 0);
            }
        }
    }
}
