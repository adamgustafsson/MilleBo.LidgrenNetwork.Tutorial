using Lidgren.Network;
using NetworkLibrary.AI.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class Enemy : Entity
    {
        public int EnemyId { get; set; }

        public int UniqueId { get; set; }

        public BaseMovement BaseMovement { get; set; }

        public Enemy(int enemyId, Position position)
            : base(position)
        {
            EnemyId = enemyId;
            UniqueId = new Random().Next(0, 90000);
            BaseMovement = new RandomMovement(Position);
        }

        public Enemy()
        {
            Position = new Position();
        }

        public override void Update(double gameTime)
        {
            BaseMovement.Update(gameTime);
        }
    }
}
