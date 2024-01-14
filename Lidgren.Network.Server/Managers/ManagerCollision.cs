using Microsoft.Xna.Framework;
using NetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer.Managers
{
    class ManagerCollision
    {
        public static bool CheckCollision(Rectangle rec, string username, List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.Username != username)
                {
                    var playerRec = new Rectangle(player.Position.XPosition, player.Position.YPosition, 100, 50);
                    if (playerRec.Intersects(rec))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
