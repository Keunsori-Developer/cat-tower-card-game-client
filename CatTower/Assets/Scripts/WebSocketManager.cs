using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using socket.io;
using System;

namespace CatTower
{
    public class WebSocketManager : SingletonGameObject<WebSocketManager>
    {
        private bool localTest = false;
        private string _url
        {
            get
            {
                if (localTest) return "http://localhost:8005";
                else return "http://cat-tower-game.herokuapp.com";
            }
        }

        //private Socket socket;
        private Dictionary<string, Socket> socketList = new Dictionary<string, Socket>();

        /// <summary>
        /// 웹소켓 통신을 요청합니다
        /// </summary>
        /// <param name="_namespace">네임스페이스 (`/rooms` or `/ingame`)</param>
        /// <param name="connectedAction">서버와 연결되었을 때 수행할 액션</param>
        public bool Connect(string _namespace, Action connectedAction = null)
        {
            Socket socket = Socket.Connect(_url + _namespace);
            if (!socketList.ContainsKey(_namespace)) socketList.Add(_namespace, socket);
            if (connectedAction != null)
            {
                socket.On(SystemEvents.connect, connectedAction);
            }
            return socket != null;
        }

        /// <summary>
        /// TODO: 수정할 예정
        /// 쓰지마세요
        /// </summary>
        /// <param name="_namespace"></param>
        /// <returns></returns>
        public bool Disconnect(string _namespace)
        {
            Socket socket;
            socketList.TryGetValue(_namespace, out socket);
            if (!socketList.TryGetValue(_namespace, out socket)) return false;
            socketList.Remove(_namespace);
            socket.Emit("disconnect");
            //socket.Disconnect();
            return true;
        }

        /// <summary>
        /// 게임 서버에 이벤트를 보냅니다
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="eventName"></param>
        /// <param name="request"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <returns></returns>
        public bool SendEvent<TRequest>(string _namespace, string eventName, TRequest request)
        {
            Socket socket;
            socketList.TryGetValue(_namespace, out socket);
            if (!socketList.TryGetValue(_namespace, out socket)) return false;

            var requestToJson = JsonUtility.ToJson(request);
            Debug.Log(eventName + " request data:\n" + requestToJson);
            socket.Emit(eventName, requestToJson.Replace("\"", "'"));
            return true;
        }

        /// <summary>
        /// 게임 서버로부터 이벤트를 받을 준비를 합니다
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="eventName"></param>
        /// <param name="responseHandler"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public bool ReceiveEvent<TResponse>(string _namespace, string eventName, Action<TResponse> responseHandler)
        {
            Socket socket;
            socketList.TryGetValue(_namespace, out socket);
            if (!socketList.TryGetValue(_namespace, out socket)) return false;
            socket.On(eventName, (string response) =>
            {
                Debug.Log(eventName + " response data:\n" + response);
                var change = response.Substring(1, response.Length - 2).Replace("\\", "");
                TResponse responseObject = JsonUtility.FromJson<TResponse>(change);
                if (responseHandler != null) responseHandler(responseObject);
            });
            return true;
        }

        /// <summary>
        /// 해당 이벤트에 대한 콜백을 더이상 받지 않도록 합니다
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool CancelToReceiveEvent(string _namespace, string eventName)
        {
            Socket socket;
            socketList.TryGetValue(_namespace, out socket);
            if (!socketList.TryGetValue(_namespace, out socket)) return false;

            socket.Off(eventName);
            return true;
        }
    }
}

// 출처: https://github.com/nhn/socket.io-client-unity3d