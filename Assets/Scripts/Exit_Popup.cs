using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatTower
{
    public class Exit_Popup : MonoBehaviour
    {
        public GameObject ExitPop;
     

        public void Exitpopupenable()
        {
            ExitPop.SetActive(true);
        }

        public void No()
        {

            ExitPop.SetActive(false);
        }

        public void Yes()
        {
            SceneManager.LoadScene("Title");
        }

    }
}
