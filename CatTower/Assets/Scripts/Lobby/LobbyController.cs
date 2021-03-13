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
        private string roomId;

        void Start()
        {
            ShowInitialLobbyUserList();
            StartButton.GetComponent<Button>().onClick.AddListener(ChangeIngame);
            WebSocketManager.Instance.ReceiveEvent<UserListResponse>("/rooms", "userlist", ReadUserList);
            WebSocketManager.Instance.ReceiveEvent<StartGameResponse>("/rooms", "start", MoveToIngame);
            exitButton.onClick.AddListener(() => { exitPopup.SetActive(true); });
        }

        void ShowInitialLobbyUserList()
        {
            printRoomName.text = JoinedRoom.roomId;
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

            if (JoinedRoom.host.mid != UserData.mid)
            {
                Debug.Log("방장이 아님");
                StartButton.gameObject.SetActive(false);
            }
            roomId = JoinedRoom.roomId;
            JoinedRoom.ClearAllData();
        }

        public void ReadUserList(UserListResponse response)
        {
            Debug.Log("userlist 이벤트 도착!!!!!!!!!!!!!!!");
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

            if (response.host.mid != UserData.mid)
            {
                Debug.Log("방장이 아님");
                StartButton.gameObject.SetActive(false);
            }
        }

        public void MoveToIngame(StartGameResponse temp)
        {
            LoadingIndicator.Hide();
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
            Debug.Log("start button이 클릭됨");
            if(UserData.mid != hostInfo.mid) return;
            LoadingIndicator.Show();
            WebSocketManager.Instance.SendEvent<ReadyByHostRequest>("/rooms", "ready",
                new ReadyByHostRequest
                {
                    hostInfo = UserData.GetUserInfo(),
                    roomId = roomId
                }
            );
        }
    }
}