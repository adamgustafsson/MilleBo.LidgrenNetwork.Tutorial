﻿using Lidgren.Network;
using Microsoft.Xna.Framework.Input;
using NetworkLibrary;
using ServerClient.MyEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerClient.Manager
{
    class ManagerNetwork
    {
        private NetClient _client;

        public string Username { get; set; }

        public string GroupId { get; private set; }

        public bool Active { get; set; }

        public event EventHandler<PlayerUpdateEventArgs> PlayerUpdateEvent;
        public event EventHandler<KickPlayerEventArgs> KickPlayerEvent;
        public event EventHandler<EnemyUpdateEventArgs> EnemyUpdateEvent;

        public bool Start()
        {
            var random = new Random();
            _client = new NetClient(new NetPeerConfiguration("networkGame"));
            _client.FlushSendQueue();
            _client.Start();
            Username = "name_" + random.Next(0, 100);
            GroupId = "test";
            var outmsg = _client.CreateMessage();
            outmsg.Write(GroupId);
            outmsg.Write((byte)PacketType.Login);
            outmsg.Write(Username);
            _client.Connect("localhost", 14241, outmsg);
            return EsablishInfo();
        }

        private bool EsablishInfo()
        {
            var time = DateTime.Now;
            NetIncomingMessage inc;
            while (true)
            {
                if (DateTime.Now.Subtract(time).Seconds > 5)
                {
                    return false;
                }
                if ((inc = _client.ReadMessage()) == null) continue;
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        if (inc.SenderConnection.Status == NetConnectionStatus.Connected)
                        {
                            Active = true;
                            return true;
                        }
                        break;
                }
            }
        }

        public void Update()
        {
            NetIncomingMessage inc;
            while ((inc = _client.ReadMessage()) != null)
            {
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        Data(inc);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        StatusChanged(inc);
                        break;
                }
            }
        }

        private void Data(NetIncomingMessage inc)
        {
            var packageType = (PacketType)inc.ReadByte();
            switch (packageType)
            {
                case PacketType.PlayerPosition:
                    var player = ReadPlayer(inc);
                    if (PlayerUpdateEvent != null)
                    {
                        PlayerUpdateEvent(this, new PlayerUpdateEventArgs(new List<Player> { player }, false));
                    }

                    break;

                case PacketType.AllPlayers:
                    ReceiveAllPlayers(inc);
                    break;

                case PacketType.Kick:
                    ReceiveKick(inc);
                    break;

                case PacketType.AllEnemies:
                    ReceiveAllEnemies(inc);
                    break;

                case PacketType.Login:
                    ReceiveAllPlayers(inc);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        private void StatusChanged(NetIncomingMessage inc)
        {
            switch ((NetConnectionStatus)inc.ReadByte())
            {
                case NetConnectionStatus.Disconnected:
                    Active = false;
                    break;
            }
        }

        private void ReceiveAllPlayers(NetIncomingMessage inc)
        {
            var list = new List<Player>();
            var cameraUpdate = inc.ReadBoolean();
            var count = inc.ReadInt32();
            for (int n = 0; n < count; n++)
            {
                list.Add(ReadPlayer(inc));
            }

            if (PlayerUpdateEvent != null)
            {
                PlayerUpdateEvent(this, new PlayerUpdateEventArgs(list, cameraUpdate));
            }
        }

        private void ReceiveAllEnemies(NetIncomingMessage inc)
        {
            var list = new List<Enemy>();
            var cameraUpdate = inc.ReadBoolean();
            var count = inc.ReadInt32();
            for (int n = 0; n < count; n++)
            {
                list.Add(ReadEnemy(inc));
            }

            if (EnemyUpdateEvent != null)
            {
                EnemyUpdateEvent(this, new EnemyUpdateEventArgs(list, cameraUpdate));
            }
        }

        private Player ReadPlayer(NetIncomingMessage inc)
        {
            var player = new Player();
            player.Username = inc.ReadString();
            inc.ReadAllProperties(player.Position);
            return player;
        }

        private Enemy ReadEnemy(NetIncomingMessage inc)
        {
            var enemy = new Enemy();
            enemy.UniqueId = inc.ReadInt32();
            enemy.EnemyId = inc.ReadInt32();
            inc.ReadAllProperties(enemy.Position);
            return enemy;
        }

        private void ReceiveKick(NetIncomingMessage inc)
        {
            var username = inc.ReadString();
            if (KickPlayerEvent != null)
            {
                KickPlayerEvent(this, new KickPlayerEventArgs(username));
            }
        }

        public void SendInput(Keys key)
        {
            var outmessage = _client.CreateMessage();
            outmessage.Write((byte)PacketType.Input);
            outmessage.Write(GroupId);
            outmessage.Write((byte)key);
            outmessage.Write(Username);
            _client.SendMessage(outmessage, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
