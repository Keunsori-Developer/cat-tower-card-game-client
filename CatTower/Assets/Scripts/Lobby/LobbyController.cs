using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatTower
{
    public class LobbyController : SingletonGameObject<LobbyController>
    {
        public Text printRoomName;
        public Text[] printUser = new Text[6];
        public GameObject ExitPopup;
        public GameObject StartButton;
        public Button exitButton;
        public GameObject exitPopup;
        private UserInfo hostInfo;

        void Start()
        {
            ShowInitialLobbyUserList();
            StartButton.SetActive(false);
            printRoomName.text = JoinedRoom.roomId;
            WebSocketManager.Instance.ReceiveEvent<UserListResponse>("/rooms", "userlist", ReadUserList);
            exitButton.onClick.AddListener(() => { exitPopup.SetActive(true); });
        }

        void ShowInitialLobbyUserList()
        {
            for (int i = 0; i < printUser.Length; i++)
            {
                printUser[i].text = "";
            }

            var userlist = JoinedRoom.joinedUserList;
            hostInfo = JoinedRoom.host;

            for (int i = 0; i < userlist.Count; i++)
            {
                printUser[i].text = userlist[i].nickname + " (" + userlist[i].mid + ")";
                if (userlist[i].mid == JoinedRoom.host.mid) printUser[i].color = Color.blue;
            }

            StartButton.SetActive(false);

            if (JoinedRoom.host.mid == UserData.mid)
            {
                Debug.Log("방장임");
                StartButton.SetActive(true);
            }

            JoinedRoom.ClearAllData();
        }

        public void ReadUserList(UserListResponse response)
        {
            for (int i = 0; i < printUser.Length; i++)
            {
                printUser[i].text = "";
            }

            var userlist = response.userList;
            hostInfo = response.host;

            for (int i = 0; i < userlist.Count; i++)
            {
                printUser[i].text = userlist[i].nickname + " (" + userlist[i].mid + ")";
                if (userlist[i].mid == response.host.mid) printUser[i].color = Color.blue;
            }

            StartButton.SetActive(false);

            if (response.host.mid == UserData.mid)
            {
                Debug.Log("방장임");
                StartButton.SetActive(true);
            }
        }

        public void MoveToIngame(StartGameResponse temp)
        {
            SceneManager.LoadScene("Ingame");
        }

        public void RequestToExitLobby()
        {
            WebSocketManager.Instance.SendEvent<ExitUserRequest>("/rooms", "exit",
               new ExitUserRequest
               {
                   roomId = JoinedRoom.roomId,
                   userInfo = UserData.GetUserInfo()
               }
            );

            SceneManager.LoadScene("Title");
        }

        public void ChangeIngame()
        {
            if(UserData.mid != hostInfo.mid) return;

            WebSocketManager.Instance.SendEvent<ReadyByHostRequest>("/rooms", "ready",
                new ReadyByHostRequest
                {
                    hostInfo = UserData.GetUserInfo(),
                    roomId = JoinedRoom.roomId
                }
            );
            
            WebSocketManager.Instance.ReceiveEvent<StartGameResponse>("/rooms", "start", MoveToIngame);
        }
    }
}