using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatTower
{
    public class HostGameController : MonoBehaviour
    {
        [SerializeField] GameObject hostGamePanel = null;
        [SerializeField] Text roomName = null;
        [SerializeField] Text capacity = null;
        [SerializeField] Text mode = null;
        //[SerializeField] GameObject roomNameError = null;
        [SerializeField] GameObject modeError = null;
        [SerializeField] GameObject capacityError = null;
        [SerializeField] Button openButton = null;
        [SerializeField] Button exitButton = null;
        [SerializeField] Button makeButton = null;

        // Start is called before the first frame update
        void Start()
        {
            //roomNameError.gameObject.SetActive(false);
            capacityError.gameObject.SetActive(false);
            modeError.gameObject.SetActive(false);
            openButton.onClick.AddListener(OpenPanel);
            exitButton.onClick.AddListener(ClosePanel);
            makeButton.onClick.AddListener(CreateRoom);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OpenPanel()
        {
            hostGamePanel.gameObject.SetActive(true);
        }
        public void ClosePanel()
        {
            hostGamePanel.gameObject.SetActive(false);
            //roomNameError.gameObject.SetActive(false);
            capacityError.gameObject.SetActive(false);
            modeError.gameObject.SetActive(false);
        }

        public void OpenHostPage()
        {
          hostGamePanel.gameObject.SetActive(true);
        }
        public void CreateRoom()
        {
            int _mode;
            capacityError.gameObject.SetActive(false);
            modeError.gameObject.SetActive(false);
            //roomNameError.gameObject.SetActive(false);
            if (int.Parse(capacity.text) < 2 || int.Parse(capacity.text) > 6)
            {
                capacityError.gameObject.SetActive(true);
                return;
            }
            // if(false)
            //{
                //roomNameError.gameObject.SetActive(true);//방이름규칙미정
                // return;
            //}
            if (int.Parse(mode.text) != 1 && int.Parse(mode.text) != 3)
            {
                modeError.gameObject.SetActive(true);
                return;
            }
            if (int.Parse(mode.text)==1)
            {
                _mode = 0;// 단판
            }
            else
            {
                _mode = 1;// 3판
            }

            /*HttpManager.Instance.Post<RoomInfoRequest, RoomInfoResponse>("/rooms/create",
            new RoomInfoRequest
            {
                hostInfo = { mid = UserData.mid, nickname = UserData.nickName },
                name = roomName.text,
                capacity = int.Parse(capacity.text),
                mode = _mode//여기서 앞모드는 클래스의 mode 뒤의 모드는 입력받는 mode
                }, JoinRoom);
                http통신부분
             */
            WebSocketManager.Instance.ReceiveEvent<JoinResponse>("/rooms", "userlist", JoinRoom);
            WebSocketManager.Instance.SendEvent<RoomInfoRequest>("/rooms", "create",
                new RoomInfoRequest
                {
                    hostInfo = new UserInfo
                    {
                        mid = UserData.mid,
                        nickname = UserData.nickName
                    },
                    name = roomName.text,
                    capacity = int.Parse(capacity.text),
                    mode = _mode//여기서 앞모드는 클래스의 mode 뒤의 모드는 입력받는 mode
                });
            //요청은 잘보내지는데 서버가팅김
            Debug.Log("요청성공. 호스트 : " + UserData.mid +" 방이름 : " + roomName.text + " 인원 : "  + int.Parse(capacity.text) + " 모드 : " + int.Parse(mode.text));//확인용
        }
        public void JoinRoom(JoinResponse room)
        {
            if (room.code == 20000)
            {
                JoinedRoom.roomId = room.roomId;
                JoinedRoom.joinedUserList = room.userList;
                JoinedRoom.host = room.host;
                Debug.Log(room.roomId + " 입장");
                //WebSocketManager.Instance.CancelToReceiveEvent("/rooms", "userlist");
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                Debug.Log("잘못된 요청");
            }
        }
    }
}