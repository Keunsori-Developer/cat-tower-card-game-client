using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatTower
{
    
    public class Get_UserInfo : MonoBehaviour
    {
     List<Userinfo> userList = new List<Userinfo>();


        List<UserInfo> userList = new List<UserInfo>();

        public Text printRoomName;
        public Text printUser0;
        public Text printUser1;
        public Text printUser2;
        public Text printUser3;
        public Text printUser4;
        public Text printUser5;
        public GameObject ExitPopup;
        public string roomLobby;
        public UserInfo hostLobby;


        void Start()
        {
            //WebSocketManager.Instance.Connect("/rooms", () =>
            //{
            WebSocketManager.Instance.ReceiveEvent<UserListResponse>("/rooms", "userlist", ReadUserList);
            //});
            //StartCoroutine(Wait_second());


        }
        /*IEnumerator Wait_second()
        {
            for (int i = 0; i < 6000; i++)//대기실은 최대 100분간 갱신됨
            {
               

                for (int k = 0; k < userList.Count; k++)
                {
                    if (k == 0)
                    {
                        printUser0.text = userList[k].nickname;//리스트 첫번째 멤버는 방장이 들어옴.
                    }
                    if (k == 1)
                    {
                        printUser1.text = userList[k].nickname;
                    }
                    if (k == 2)
                    {
                        printUser2.text = userList[k].nickname;
                    }
                    if (k == 3)
                    {
                        printUser3.text = userList[k].nickname;
                    }
                    if (k == 4)
                    {
                        printUser4.text = userList[k].nickname;
                    }
                    if (k == 5)
                    {
                        printUser5.text = userList[k].nickname;
                    }
                    printRoomName.text = UserListResponse.roomid;
                        

                }
                yield return new WaitForSeconds(1);
            }

        }*/

        public void ReadUserList(UserListResponse temp)
        {
            //0번째 인덱스 멤버는 방장
            roomLobby = temp.roomId;
            hostLobby = temp.host;
            int i = 0;
            foreach (Userinfo user in temp.user)
            {
                if (i == 0)
                {
                    userList.Add(new Userinfo() { mid = temp.host.mid, nickname = temp.host.nickname, });
                }
                else
                {
                    userList.Add(new Userinfo() { mid = temp.user[i].mid, nickname = temp.user[i].nickname, });
                }//userList1~5번 인덱스에 참가자 정보 넣음
                i++;
            }
            for (int k = 0; k < userList.Count; k++)
            {
                if (k == 0)
                {
                    printUser0.text = userList[k].nickname;//리스트 첫번째 멤버는 방장이 들어옴.
                    
                }
                if (k == 1)
                {
                    printUser1.text = userList[k].nickname;
                }
                if (k == 2)
                {
                    printUser2.text = userList[k].nickname;
                }
                if (k == 3)
                {
                    printUser3.text = userList[k].nickname;
                }
                if (k == 4)
                {
                    printUser4.text = userList[k].nickname;
                }
                if (k == 5)
                {
                    printUser5.text = userList[k].nickname;
                }
                printRoomName.text = temp.roomId;


            }

        }


        public class UserListResponse
        {

            public List<Userinfo> user;
            public string roomid;
            public Userinfo host;

            public List<UserInfo> user;
            public string roomId;
            public UserInfo host;
            

        }
        
        public class ExitUserResponse
        {

            public Userinfo user;
            public string roomid;

            public UserInfo userInfo;
            public string roomId;


        }
        public class ReadyUserResponse
        {
            public UserInfo hostInfo;
            public string roomId;


        }
        public class StartUserResponse
        {

            public Userinfo user;
            public string roomid;

            
            public string roomId;


        }
        public void Yes()
        {
            WebSocketManager.Instance.Connect("/rooms", () =>
           {
               WebSocketManager.Instance.SendEvent<ExitUserResponse>("/rooms", "exit",

                  new ExitUserResponse
                  {

                      roomId = roomLobby,
                      userInfo = {
                          nickname = UserData.nickName, mid = UserData.mid 
                      }

                  });

                SceneManager.LoadScene("Title");
           });



        }
        
        public void ChangeIngame()
        {
            WebSocketManager.Instance.SendEvent<ReadyUserResponse>("/rooms", "Ready",

                new ReadyUserResponse
                {

                    hostInfo = hostLobby,
                    roomId = roomLobby

                }) ;
            WebSocketManager.Instance.SendEvent<StartUserResponse>("/rooms", "start",

               new StartUserResponse
               {


                   roomId = roomLobby

               });

            SceneManager.LoadScene("Ingame");
        }


    }
}

