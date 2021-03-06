using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CatTower
{
    public class JoinGameController : MonoBehaviour
    {
        [SerializeField] GameObject joinGamePanel = null;
        [SerializeField] GameObject roomPrefap = null;
        [SerializeField] GameObject prefapParents = null;
        [SerializeField] Text roomCode = null;
        [SerializeField] GameObject joinWarnText = null;
        [SerializeField] Button openButton = null;
        [SerializeField] Button refreshButton = null;
        [SerializeField] Button exitButton = null;
        [SerializeField] Button codePanelButton = null;
        [SerializeField] Button codeJoinButton = null;
        [SerializeField] Button exitCodeButton = null;
        [SerializeField] GameObject codeInputPanel = null;
        [SerializeField] GameObject errorCanvas = null;
        [SerializeField] Button exitError = null;
        List<RoomInfo> roomList = new List<RoomInfo>();

        // Start is called before the first frame update
        void Start()
        {
            joinWarnText.gameObject.SetActive(false);
            openButton.onClick.AddListener(OpenJoinPanel);
            openButton.onClick.AddListener(RefreshRoomList);
            refreshButton.onClick.AddListener(RefreshRoomList);
            exitButton.onClick.AddListener(CloseJoinPanel);
            codePanelButton.onClick.AddListener(OpenCodePanel);
            codeJoinButton.onClick.AddListener(JoinRoomByCode);
            exitCodeButton.onClick.AddListener(CloseCodePanel);
            exitError.onClick.AddListener(ErrorClose);
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CloseJoinPanel()
        {
            joinGamePanel.gameObject.SetActive(false);
            joinWarnText.gameObject.SetActive(false);
        }
        public void OpenCodePanel()
        {
            codeInputPanel.gameObject.SetActive(true);
        }
        public void CloseCodePanel()
        {
            codeInputPanel.gameObject.SetActive(false);
            joinWarnText.gameObject.SetActive(false);
        }
        public void OpenJoinPanel()
        {
            joinGamePanel.gameObject.SetActive(true);
            RefreshRoomList();
        }
        public void CreateRoomList(RoomInfo RL)
        {

            GameObject clone = Instantiate(roomPrefap, prefapParents.transform) as GameObject;//대충 새로만들고 복사한다는내용
            clone.name = RL.name;
            clone.transform.Find("RoomName").GetComponentInChildren<Text>().text = RL.name;
            clone.transform.Find("RoomId").GetComponentInChildren<Text>().text = RL.id;
            clone.transform.Find("CurrentMember").GetComponentInChildren<Text>().text = RL.joined.ToString();
            clone.transform.Find("Capacity").GetComponentInChildren<Text>().text = RL.capacity.ToString();
            Button join = clone.GetComponentInChildren<Button>();//프리팹 join 버튼 연결
            join.onClick.AddListener(() => JoinRoomByClick(clone));
        }//낱개 프리팹 복사하는함수
        public void RefreshRoomList()
        {
            roomList.RemoveAll(x => true);
            Transform _previousList = prefapParents.GetComponentInChildren<Transform>();
            foreach (Transform _previousChild in _previousList)
            {
                if (_previousChild != _previousList)
                {
                    Destroy(_previousChild.gameObject);
                }
            }//기존에 생성된 프리팹들 삭제
            WebSocketManager.Instance.ReceiveEvent<RoomListResponse>("/rooms", "active", ReadRoomList);//잠시오픈
            WebSocketManager.Instance.SendEvent<RoomListRequest>("/rooms", "roomlist",(null));//요청
        }
        public void ReadRoomList(RoomListResponse temp) 
        {
            int i = 0;
            if (ErrorDetect(temp.code) == false)
            {
                WebSocketManager.Instance.CancelToReceiveEvent("/rooms", "active");
                return;
            }
            foreach(RoomInfo room in temp.rooms)
            {
                roomList.Add(new RoomInfo() { capacity = temp.rooms[i].capacity, mode = temp.rooms[i].mode, id = temp.rooms[i].id, joined = temp.rooms[i].joined, name = temp.rooms[i].name });
                //Debug.Log("RoomListAdded");
                i++;
            }
            for (i = 0; i < roomList.Count; i++)
            {
                CreateRoomList(roomList[i]);
                //Debug.Log("ID : " + roomList[i].id + " 이름 : " + roomList[i].name + " " + roomList[i].joined + "/" + roomList[i].capacity);
            }//리스트 만드는거
            WebSocketManager.Instance.CancelToReceiveEvent("/rooms", "active");//다 받고 종료
        }

        public void JoinRoomByClick(GameObject room)
        {
            Debug.Log("클릭join요청 , roomid " + room.transform.Find("RoomId").GetComponentInChildren<Text>().text + " userinfo " + UserData.nickName + " id " + UserData.mid);
            WebSocketManager.Instance.ReceiveEvent<JoinResponse>("/rooms","userlist",JoinRoom);
            WebSocketManager.Instance.SendEvent<JoinRequest>("/rooms", "join",
                new JoinRequest
                {
                    roomId = room.transform.Find("RoomId").GetComponentInChildren<Text>().text,
                    userInfo = { nickname = UserData.nickName, mid = UserData.mid }
                });
        }//버튼클릭으로 입장
        public void JoinRoomByCode()
        {
            if (roomCode.text == "")
            {
                joinWarnText.gameObject.SetActive(true);
                return;
            }
            Debug.Log("코드join요청 , roomid " + roomCode.text + " Userinfo " + UserData.nickName + " id " + UserData.mid);
            WebSocketManager.Instance.ReceiveEvent<JoinResponse>("/rooms", "userlist", JoinRoom);
            WebSocketManager.Instance.SendEvent<JoinRequest>("/rooms", "join",
                new JoinRequest
                {
                    roomId = roomCode.text.ToUpper(),
                    userInfo = { nickname = UserData.nickName, mid = UserData.mid }
                });
        }//방 코드입력으로 입장
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
                CloseJoinPanel();
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
                if(code == 40000)
                {
                    ErrorMessage("Error 40000\n잘못된 요청입니다.");
                }
                else if(code == 40101)
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