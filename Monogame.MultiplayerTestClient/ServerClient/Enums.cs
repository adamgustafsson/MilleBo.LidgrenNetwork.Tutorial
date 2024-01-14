using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient
{
    public enum ComponentType
    {
        Sprite,
        PlayerInput,
        Animation,
        Collision,
        MainPlayer,
        Name
    }

    public enum Input
    {
        Left,
        Right,
        Up,
        Down,
        None,
        Enter,
        A,
        S,
        Select,
        Start
    }

    public enum State
    {
        Standing,
        Walking,
        Special
    }
}
