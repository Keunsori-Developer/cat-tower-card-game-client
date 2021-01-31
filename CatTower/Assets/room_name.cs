using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class room_name : MonoBehaviour
    {
        public Text roomName;
        // Start is called before the first frame update
        void Start()
        {
            roomName.text = roomId;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}