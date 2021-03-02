using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatTower {
    public class SceneChange : MonoBehaviour
    {
        public GameObject ExitPopup;
        public void ChangeLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
        //public void ChangeIngame()
        //{
          //  SceneManager.LoadScene("Ingame");
        //}
        public void ChangeRoomSelect()
        {
            SceneManager.LoadScene("Title");
        }
        public void ChangeSetting()
        {
            SceneManager.LoadScene("Setting");
        }
        public void Exitpopupenable()
        {
            ExitPopup.SetActive(true);
        }

        public void No()
        {

            ExitPopup.SetActive(false);
        }

        public void Yes()
        {
           
            SceneManager.LoadScene("Title");
        }

    }
}

  
