using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.AI.Movement
{
    public abstract class BaseMovement
    {
        protected int Speed;
        public Position Position { get; set; }

        protected BaseMovement(Position position)
        {
            Position = position;
            Speed = 1;
        }

        public abstract void Update(double gameTime);
    }
}
