using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class ExitPopup : MonoBehaviour
    {
        [SerializeField] Button yesButton;
        [SerializeField] Button noButton;

        // Start is called before the first frame update
        void Start()
        {
            yesButton.onClick.AddListener(LobbyController.Instance.RequestToExitLobby);
            noButton.onClick.AddListener(() => { this.gameObject.SetActive(false); });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}