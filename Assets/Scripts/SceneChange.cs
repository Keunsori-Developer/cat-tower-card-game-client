using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatTower
{

    public class SceneChange : MonoBehaviour
    {

        public void ChangeLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
        public void ChangeIngame()
        {
            SceneManager.LoadScene("InGame");
        }
        public void ChangeRoomSelect()
        {
            SceneManager.LoadScene("Room_Select_Example");
        }
        public void ChangeSetting()
        {
            SceneManager.LoadScene("Setting");
        }

    }
}

  
