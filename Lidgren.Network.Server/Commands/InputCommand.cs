﻿using Lidgren.Network;
using LidgrenServer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer.Commands
{
    class InputCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", "Received new input");
            var key = (Keys)inc.ReadByte();
            var name = inc.ReadString();
            playerAndConnection = gameRoom.Players.FirstOrDefault(p => p.Player.Username == name);
            if (playerAndConnection == null)
            {
                managerLogger.AddLogMessage("server", string.Format("Could not find player with name {0}", name));
                return;
            }

            int x = 0;
            int y = 0;

            switch (key)
            {
                case Keys.Down:
                    y++;
                    break;

                case Keys.Up:
                    y--;
                    break;

                case Keys.Left:
                    x--;
                    break;

                case Keys.Right:
                    x++;
                    break;
            }

            var player = playerAndConnection.Player;
            var position = playerAndConnection.Player.Position;
            if (!ManagerCollision.CheckCollision(new Rectangle(position.XPosition + x, position.YPosition + y, 100, 50),
                player.Username, gameRoom.Players.Select(p => p.Player).ToList()))
            {
                position.XPosition += x;
                position.YPosition += y;

                position.Visible = gameRoom.ManagerCamera.InScreenCheck(new Vector2(position.XPosition, position.YPosition));
                if (position.Visible)
                {
                    var screenPosition =
                        gameRoom.ManagerCamera.WorldToScreenPosition(new Vector2(position.XPosition, position.YPosition));
                    position.ScreenXPosition = (int)screenPosition.X;
                    position.ScreenYPosition = (int)screenPosition.Y;
                }

                var command = new PlayerPositionCommand();
                command.Run(managerLogger, server, inc, playerAndConnection, gameRoom);
            }
        }
    }
}
