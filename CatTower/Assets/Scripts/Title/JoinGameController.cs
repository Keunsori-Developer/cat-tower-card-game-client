using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class JoinGameController : MonoBehaviour
    {
        [SerializeField] GameObject joinGamePanel = null;
        [SerializeField] GameObject roomPrefap = null;
        [SerializeField] GameObject prefapParents = null;
        [SerializeField] Text roomCode = null;
        List<RoomListResponse> roomList = new List<RoomListResponse>();
        int _dummyNo;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OpenJoinPage()
        {
            joinGamePanel.gameObject.SetActive(true);
            RefreshRoomList();
        }
        public void CreateRoomList(RoomListResponse RL)
        {

            GameObject clone = Instantiate(roomPrefap, Vector3.zero, Quaternion.identity) as GameObject;//대충 새로만들고 복사한다는내용
            clone.transform.SetParent(prefapParents.transform);
            clone.name = RL.name;
            clone.transform.Find("RoomName").GetComponentInChildren<Text>().text = RL.name;
            clone.transform.Find("RoomId").GetComponentInChildren<Text>().text = RL.id;
            clone.transform.Find("CurrentMember").GetComponentInChildren<Text>().text = RL.joined.ToString();
            clone.transform.Find("Capacity").GetComponentInChildren<Text>().text = RL.capacity.ToString();
            Button join = clone.GetComponentInChildren<Button>();//프리팹 join 버튼 연결
            //join.onClick.AddListener();
        }//낱개 프리팹 복사하는함수
        public void RefreshRoomList()
        {
            Transform _previousList = prefapParents.GetComponentInChildren<Transform>();
            foreach (Transform _previousChild in _previousList)
            {
                if (_previousChild != _previousList)
                {
                    Destroy(_previousChild.gameObject);
                }
            }//기존에 생성된 프리팹들 삭제

            HttpManager.Instance.Get<List<RoomListResponse>>("/rooms/active", ReadRoomList);// 방정보 Get  
            
            for (int i = 0; i < roomList.Count; i++)
            {
                CreateRoomList(roomList[i]);
                Debug.Log("ID : "+ roomList[i].id+ " 이름 : "+ roomList[i].name+ " "+ roomList[i].joined+ "/"+ roomList[i].capacity);
            }//리스트 만드는거
        }
        public void Makedummy()
        {
            roomList.Add(new RoomListResponse() { name = _dummyNo.ToString(), id = _dummyNo.ToString(), capacity = 4, joined = 3 });
            Debug.Log("made dummy : " + _dummyNo);
            _dummyNo++;
        }//방목록 api받는거 아직 미완성이라 일단 실험용으로 만든거
        public void ReadRoomList(List<RoomListResponse> temp) 
        {
            roomList.Add(new RoomListResponse() { name = temp[1].name, id = temp[1].id, capacity = temp[1].capacity, joined = temp[1].joined });
        }// Get으로 가져온거 그대로 복사하는 함수

        public void JoinRoomByClick(Button join)
        {
            Debug.Log("test");
            HttpManager.Instance.Post<JoinRequest, JoinResponse>("/rooms/join",
            new JoinRequest
            {
                roomId = "a",
                userInfo = { nickname = UserInfo.nickName, mid = UserInfo.mid }
            }, null) ;
        }
        public void JoinRoomByCode()
        {
            HttpManager.Instance.Post<JoinRequest, JoinResponse>("/rooms/join",
            new JoinRequest
            {
                roomId = roomCode.text,
                userInfo = {nickname = UserInfo.nickName, mid = UserInfo.mid }
            }, null);
        }//방 코드입력으로 입장
    }
}