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
        public Text printRoomName;
        public Text printUser0;
        public Text printUser1;
        public Text printUser2;
        public Text printUser3;
        public Text printUser4;
        public Text printUser5;
        public GameObject ExitPopup;



        void Start()
        {

            StartCoroutine(Wait_second());
          

        }
        IEnumerator Wait_second()
        {
            for (int i = 0; i < 6000; i++)//대기실은 최대 100분간 갱신됨
            {
                //WebSocketManager.Instance.Connect("/rooms", () =>
                //{
                    WebSocketManager.Instance.ReceiveEvent<UserListResponse>("/rooms", "userlist", ReadUserList);
                //});

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
                    //printRoomName.text = userList[k].roomid;
                        

                }
                yield return new WaitForSeconds(1);
            }

        }

        public void ReadUserList(UserListResponse temp)
        {
            userList.Add(new Userinfo() { });//0번째 인덱스 멤버는 방장
            int i = 1;
            foreach (Userinfo room in temp.user)
            {
                userList.Add(new Userinfo() { mid = temp.user[i].mid, nickname = temp.user[i].nickname, });//userList1~5번 인덱스에 참가자 정보 넣음
               i++;
            }
           
        }
       

        public class UserListResponse
        {
            public List<Userinfo> user;
            public string roomid;
            public Userinfo host;//여기 있는 정보들 어디파일에서 받아와야되는지 잘 모르겠다..
        }
        public void Yes()
        {
            WebSocketManager.Instance.Connect("/rooms", () =>
            {
                WebSocketManager.Instance.SendEvent<UserListResponse>("/rooms", "exit", );//Request parameters 전달해야됨.
            });
            SceneManager.LoadScene("Title");
        }
        


    }
   

}
