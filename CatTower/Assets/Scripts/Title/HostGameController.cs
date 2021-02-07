using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{ 
    public class HostGameController : MonoBehaviour
    {
        [SerializeField] GameObject hostGamePanel = null;
        [SerializeField] Text roomName = null;
        [SerializeField] Text capacity = null;
      //[SerializeField] GameObject roomNameError = null;
        [SerializeField] GameObject capacityError = null;

        int _capacity = 0;
        string _roomName = "";

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
    {
        
    }

        public void OpenHostPage()
        {
          hostGamePanel.gameObject.SetActive(true);
        }
        public void CreateRoom()
        {
            _capacity = int.Parse(capacity.text);
            _roomName = roomName.text;
            if (_capacity < 2 || _capacity > 6)
            {
                capacityError.gameObject.SetActive(true);
                /*
                if(_roomname)
                {
                    roomNameError.gameObject.SetActive(true);
                    return;
                }
                방 이름 규칙관해서 아직 안정해서 주석으로만 남겨놓음
                */
                return;
            }
                HttpManager.Instance.Post<RoomInfoRequest, RoomInfoResponse>("/rooms/create",
                new RoomInfoRequest
                {
                    hostId = UserInfo.mid,
                    name = _roomName,
                    capacity = _capacity
                }, null) ;
            Debug.Log("요청성공. 호스트 : " + UserInfo.mid +" 방이름 : " + _roomName + " 인원 : "  + _capacity);//확인용
        }
    }
}