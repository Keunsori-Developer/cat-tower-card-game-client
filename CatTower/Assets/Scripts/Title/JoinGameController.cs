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
        List<RoomInfo> roomList = new List<RoomInfo>();

        // RoomListResponse roomList = new RoomListResponse();
        int _dummyNo;

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
            //HttpManager.Instance.Get<RoomListResponse>("/rooms/active", ReadRoomList);// 방정보 Get후 생성 (코루틴으로 불러오는거라 저 함수서 처리못하면 생성이안됨)
            WebSocketManager.Instance.SendEvent<RoomListRequest>("/rooms", "roomlist",(null));//요청
        }
        public void Makedummy()
        {
            roomList.Add(new RoomInfo() { name = _dummyNo.ToString(), id = _dummyNo.ToString(), capacity = 4, joined = 3, });
            Debug.Log("made dummy : " + _dummyNo);
            _dummyNo++;
        }//방목록 api받는거 아직 미완성이라 일단 실험용으로 만든거
        public void ReadRoomList(RoomListResponse temp) 
        {
            int i = 0;
            foreach(RoomInfo room in temp.rooms)
            {
                roomList.Add(new RoomInfo() { capacity = temp.rooms[i].capacity, mode = temp.rooms[i].mode, id = temp.rooms[i].id, joined = temp.rooms[i].joined, name = temp.rooms[i].name });
                Debug.Log("RoomListAdded");
                i++;
            }
            for (i = 0; i < roomList.Count; i++)
            {
                CreateRoomList(roomList[i]);
                Debug.Log("ID : " + roomList[i].id + " 이름 : " + roomList[i].name + " " + roomList[i].joined + "/" + roomList[i].capacity);
            }//리스트 만드는거
            WebSocketManager.Instance.CancelToReceiveEvent("/rooms", "active");//다 받고 종료
        }

        public void JoinRoomByClick(GameObject room)
        {
            Debug.Log("클릭join요청 , roomid " + room.transform.Find("RoomId").GetComponentInChildren<Text>().text + " userinfo " + UserData.nickName + " id " + UserData.mid);
            /* HttpManager.Instance.Post<JoinRequest, JoinResponse>("/rooms/join",
            new JoinRequest
            {
                roomId = room.transform.Find("RoomId").GetComponentInChildren<Text>().text,
                userInfo = { nickname = UserData.nickName, mid = UserData.mid }
            }, JoinRoom) ;
            */
            LoadingIndicator.Show();
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
            Debug.Log("코드join요청 , roomid " + roomCode.text + " Userinfo " + UserData.nickName + " id " + UserData.mid);
            /*HttpManager.Instance.Post<JoinRequest, JoinResponse>("/rooms/join",
            new JoinRequest
            {
                roomId = roomCode.text.ToUpper(),
                userInfo = { nickname = UserData.nickName, mid = UserData.mid }
            }, JoinRoom); ;
            */
            WebSocketManager.Instance.SendEvent<JoinRequest>("/rooms", "join",
                new JoinRequest
                {
                    roomId = roomCode.text.ToUpper(),
                    userInfo = { nickname = UserData.nickName, mid = UserData.mid }
                });
        }//방 코드입력으로 입장
        public void JoinRoom(JoinResponse room)
        {
            LoadingIndicator.Hide();
            if (room.code == 20000)
            {
                JoinedRoom.roomId = room.roomId;
                JoinedRoom.joinedUserList = room.userList;
                JoinedRoom.host = room.host;
                Debug.Log(room.roomId + " 입장");
                WebSocketManager.Instance.CancelToReceiveEvent("/rooms", "userlist");
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                if (joinWarnText.activeSelf == false)
                    joinWarnText.SetActive(true);
                Debug.Log("잘못된 요청");
            }
        }
    }
}