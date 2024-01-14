using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    public class Entity
    {
        public Position Position { get; set; }

        public Entity(Position position)
        {
            Position = position;
        }

        public Entity() { }

        public virtual void Update(double gameTime)
        {

        }
    }
}
