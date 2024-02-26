using Colyseus;
using System;
using System.Collections.Generic;

namespace Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        private ColyseusRoom<State> _room;

        public event Action<string> SceneSynchronized;
        public event Action<string> RoomFound;

        public string ClientID => _room == null ? "" : _room.SessionId;
        public string SessionId => _room.SessionId;
        public string RoomId => _room.RoomId;

        public void FindGame(string login)
        {
            InitializeClient();
            ConnectClient(login);
        }

        public void FindGame(string login, string id)
        {
            InitializeClient();
            ConnectClientById(login, id);
        }

        public bool TrySendMessage(string key, object message)
        {
            bool isCorrectMessage = MessagesNames.DetermineMessageCorrectness(key, message);

            if (isCorrectMessage)
                _room.Send(key, message);

            return isCorrectMessage;
        }

        public void Leave()
        {
            _room?.Leave();
            _room = null;
        }

        private async void ConnectClient(string playerLogin)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {MessagesNames.Login, playerLogin }
            };

            _room = await client.JoinOrCreate<State>(StatesNames.GameRoomName, data);
            RoomFound?.Invoke(RoomId);

            SubscribeMessages();
        }

        private async void ConnectClientById(string playerLogin, string id)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {MessagesNames.Login, playerLogin }
            };

            _room = await client.JoinById<State>(id, data);
            RoomFound?.Invoke(RoomId);

            SubscribeMessages();
        }

        private void SubscribeMessages()
        {
            _room.OnMessage<string>(MessagesNames.Synchronize, jsonSynchronizeData => SceneSynchronized?.Invoke(jsonSynchronizeData));

            //_room.OnJoin += OnRoomJoined;
            _room.OnStateChange += OnStateChanged;
        }

        private void OnStateChanged(State state, bool isFirstState)
        {
        }
    }
}