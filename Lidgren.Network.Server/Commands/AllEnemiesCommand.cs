﻿using Lidgren.Network;
using LidgrenServer.Managers;
using NetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer.Commands
{
    class AllEnemiesCommand : ICommand
    {
        public bool CameraUpdate { get; set; }

        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", "Sending enemy list");
            var outmessage = server.NetServer.CreateMessage();
            outmessage.Write((byte)PacketType.AllEnemies);
            outmessage.Write(CameraUpdate);
            outmessage.Write(gameRoom.Enemies.Count);
            foreach (var e in gameRoom.Enemies)
            {
                outmessage.Write(e.UniqueId);
                outmessage.Write(e.EnemyId);
                outmessage.WriteAllProperties(e.Position);
            }
            server.NetServer.SendMessage(outmessage, gameRoom.Players.Select(p => p.Connection).ToList(), NetDeliveryMethod.ReliableOrdered, 0);
        }
    }
}
