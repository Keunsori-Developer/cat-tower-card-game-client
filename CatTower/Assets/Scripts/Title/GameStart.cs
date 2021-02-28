using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatTower
{
    public class GameStart : MonoBehaviour
    {
        private WebSocketManager webSocket;
        // Start is called before the first frame update
        void Start()
        {
            webSocket = WebSocketManager.Instance;
            webSocket.Connect("/rooms", () =>
            {

            });
            
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}