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
            LoadingIndicator.Show();
            webSocket = WebSocketManager.Instance;
            webSocket.Connect("/rooms", () =>
            {
                Debug.Log("connected");
                LoadingIndicator.Hide();
            });
            
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}