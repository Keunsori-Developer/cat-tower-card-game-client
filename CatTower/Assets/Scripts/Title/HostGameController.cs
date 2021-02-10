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
        [SerializeField] Text mode = null;
        //[SerializeField] GameObject roomNameError = null;
        [SerializeField] GameObject capacityError = null;


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
            if (int.Parse(capacity.text) < 2 || int.Parse(capacity.text) > 6)
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
                    name = roomName.text,
                    capacity = int.Parse(capacity.text),
                    mode = int.Parse(mode.text)//여기서 앞모드는 클래스의 mode 뒤의 모드는 입력받는 mode
                }, null) ;
            Debug.Log("요청성공. 호스트 : " + UserInfo.mid +" 방이름 : " + roomName.text + " 인원 : "  + int.Parse(capacity.text) + " 모드 : " + int.Parse(mode.text));//확인용
        }
    }
}