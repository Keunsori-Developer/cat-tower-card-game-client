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
        [SerializeField] GameObject roomNameError = null;
        [SerializeField] GameObject capacityError = null;
        [SerializeField] Button openButton = null;
        [SerializeField] Button exitButton = null;
        [SerializeField] Button makeButton = null;
        [SerializeField] Toggle Mode0 = null;
        [SerializeField] Toggle Mode1 = null;
        [SerializeField] GameObject errorCanvas = null;
        [SerializeField] Button exitError = null;
        bool error = false;

        // Start is called before the first frame update
        void Start()
        {
            //roomNameError.gameObject.SetActive(false);
            capacityError.gameObject.SetActive(false);
            openButton.onClick.AddListener(OpenPanel);
            exitButton.onClick.AddListener(ClosePanel);
            makeButton.onClick.AddListener(CreateRoom);
            Mode0.onValueChanged.AddListener(ToggleMode0);
            Mode1.onValueChanged.AddListener(ToggleMode1);
            Mode0.isOn = true;
            Mode1.isOn = false;
            exitError.onClick.AddListener(ErrorClose);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OpenPanel()
        {
            hostGamePanel.gameObject.SetActive(true);
            capacityError.gameObject.SetActive(false);
            roomNameError.gameObject.SetActive(false);
        }
        public void ClosePanel()
        {
            hostGamePanel.gameObject.SetActive(false);
            roomNameError.gameObject.SetActive(false);
            capacityError.gameObject.SetActive(false);
        }
        public void ToggleMode0(bool a)
        {
            if (a == true)
            {
                Mode1.isOn = false;
            }
            else
                Mode1.isOn = true;
        }
        public void ToggleMode1(bool a)
        {
            if(a==true)
                Mode0.isOn = false;
            else
                Mode0.isOn = true;
        }

        public void CreateRoom()
        {
            int _mode = 0;
            capacityError.gameObject.SetActive(false);
            roomNameError.gameObject.SetActive(false);
            error = false;
            if (int.Parse(capacity.text) < 2 || int.Parse(capacity.text) > 6 || capacity.text == null)
            {
                capacityError.gameObject.SetActive(true);
                error = true;
            }
             if(roomName.text == null)
            {
                roomNameError.gameObject.SetActive(true);//방이름상세규칙미정
                error = true;
            }

            if (Mode0.isOn == true) //모드 토글 확인
                _mode = 0;// 단판
            else if (Mode1.isOn == true)
                _mode = 1;// 3판
            else
                error = true;

            if (error == true)//중간에 에러검출시 리턴
                return;

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
            Debug.Log("요청성공. 호스트 : " + UserData.mid +" 방이름 : " + roomName.text + " 인원 : "  + int.Parse(capacity.text) + " 모드 : " + _mode);//확인용
        }
        public void JoinRoom(JoinResponse room)
        {
            if (ErrorDetect(room.code) == true)
            {
                JoinedRoom.roomId = room.roomId;
                Debug.Log(room.roomId + " 입장");
                //WebSocketManager.Instance.CancelToReceiveEvent("/rooms", "userlist");
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                ClosePanel();//에러메시지출력후 호스트창 종료
            }
        }
        public bool ErrorDetect(int code)
        {
            if (code == 20000)
            {
                return true;
            }
            else
            {
                if (code == 40000)
                {
                    ErrorMessage("Error 40000\n잘못된 요청입니다.");
                }
                else if (code == 40101)
                {
                    ErrorMessage("Error 40101\n해당 방정보가 존재하지 않습니다.");
                }
                else if (code == 40102)
                {
                    ErrorMessage("Error 40102\n인원이 가득 찼습니다.");
                }
                else if (code == 40103)
                {
                    ErrorMessage("Error 40102\n이미 방에 진입했습니다.");
                }
                else if (code == 50000)
                {
                    ErrorMessage("Error 50000\n서버에 문제가 발생했습니다.");
                }
                else
                {
                    ErrorMessage("Error " + code.ToString() + "\n알려지지 않은 에러");
                }
                return false;
            }
        }
        public void ErrorMessage(string message)
        {
            errorCanvas.gameObject.SetActive(true);
            errorCanvas.transform.Find("ErrorMessage").GetComponentInChildren<Text>().text = message;
        }
        public void ErrorClose()
        {
            errorCanvas.gameObject.SetActive(false);

        }
    }
}