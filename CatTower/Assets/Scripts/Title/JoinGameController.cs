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
        public void CreateRoomList(RoomListResponse RL)//프리팹 한개 복사하는함수
        {
            GameObject clone = Instantiate(roomPrefap, Vector3.zero, Quaternion.identity) as GameObject;//대충 새로만들고 복사한다는내용
            clone.transform.SetParent(prefapParents.transform);
            clone.name = RL.name;
            clone.transform.FindChild("RoomName").GetComponentInChildren<Text>().text=RL.name;
            clone.transform.FindChild("RoomId").GetComponentInChildren<Text>().text = RL.id;
            clone.transform.FindChild("CurrentMember").GetComponentInChildren<Text>().text = RL.joined.ToString();
            clone.transform.FindChild("Capacity").GetComponentInChildren<Text>().text = RL.capacity.ToString();
        }
        public void RefreshRoomList()
        {
            Transform _previousList = prefapParents.GetComponentInChildren<Transform>();//기존 창 날리는거
            foreach (Transform _previousChild in _previousList)
            {
                if(_previousChild!=_previousList)
                {
                    Destroy(_previousChild.gameObject);
                }
            }

            // api를 날리고 받는거 미구현

            for (int i = 0; i < roomList.Count; i++)
            {
                CreateRoomList(roomList[i]);
            }//리스트 만드는거
        }
        public void Makedummy()//방목록 api받는거 아직 미완성이라 일단 실험용으로 만든거
        {
            roomList.Add(new RoomListResponse() { name = _dummyNo.ToString(), id = _dummyNo.ToString(), capacity = 4, joined = 3 });
            Debug.Log("made dummy : " + _dummyNo);
            _dummyNo++;
        }

        public void JoinRoom()
        {

        }

    }
}