using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class Player : Entity
    {
        public string Username { get; set; }

        public Player(string username, Position position)
            : base(position)
        {
            Username = username;
        }

        public Player()
        {
            Position = new Position();
        }

    }
}
