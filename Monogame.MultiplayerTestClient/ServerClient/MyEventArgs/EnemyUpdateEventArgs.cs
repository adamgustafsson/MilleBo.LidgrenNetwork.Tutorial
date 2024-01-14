using NetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClient.MyEventArgs
{
    class EnemyUpdateEventArgs : EventArgs
    {
        public List<Enemy> Enemies { get; set; }
        public bool CameraUpdate { get; set; }

        public EnemyUpdateEventArgs(List<Enemy> enemies, bool cameraUpdate)
        {
            Enemies = enemies;
            CameraUpdate = cameraUpdate;
        }

    }
}
