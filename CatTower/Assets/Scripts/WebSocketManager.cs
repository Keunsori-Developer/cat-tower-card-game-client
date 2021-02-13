using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace CatTower
{
    public class WebSocketManager : SingletonGameObject<WebSocketManager>
    {
        private string _url = "ws://localhost:8005";
        private WebSocket webSocket;

        void Start() {
            webSocket = new WebSocket(_url + "/ingame");
            webSocket.OnOpen += (sender, e) => {
                Debug.Log(sender + " / " + e);
            };
            webSocket.OnMessage += (sender, e) => {
                Debug.Log("!!!!!!"+ sender);
            };
            webSocket.Connect();
            Debug.Log("???");
            webSocket.Send("!!!!!!!!!!!!!");
        }
    }
}

// 출처: https://github.com/sta/websocket-sharp