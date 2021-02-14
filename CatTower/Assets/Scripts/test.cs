using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class test : MonoBehaviour
    {
        WebSocketManager webSocket;

        // Start is called before the first frame update
        void Start()
        {
            
             WebSocketManager.Instance.Connect("/rooms", () =>
            {
                // 진짜 예제이므로 UserListResponse는 쓰지마세요
                // /rooms 네임스페이스에 속한 userlist 라는 이벤트를 받겠다라고 선언하는 것임
                WebSocketManager.Instance.ReceiveEvent<UserListResponse>("/rooms", "userlist", null);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    [System.Serializable]
    /// <summary>
    /// 예제용으로 넣은거니 쓰지마세요
    /// </summary>
    public class UserListResponse
    {
        public string userlist;
    }
}