using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public enum PacketType
    {
        Login,
        PlayerPosition,
        AllPlayers,
        Input,
        Kick,
        CameraUpdate,
        AddEnemy,
        AllEnemies
    }

}
