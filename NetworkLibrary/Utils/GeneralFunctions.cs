using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Utils
{
    //BAD NAME
    class GeneralFunctions
    {
        public static double Distance(Position mainObjectPosition, Position targetObjectPosition)
        {
            var x = Math.Pow(mainObjectPosition.XPosition - targetObjectPosition.XPosition, 2);
            var y = Math.Pow(mainObjectPosition.YPosition - targetObjectPosition.YPosition, 2);
            return Math.Sqrt(x + y);
        }
    }
}
