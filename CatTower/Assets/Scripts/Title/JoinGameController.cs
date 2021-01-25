using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class JoinGameController : MonoBehaviour
    {
        [SerializeField] GameObject joinGamePanel = null;
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
        }
    }
}