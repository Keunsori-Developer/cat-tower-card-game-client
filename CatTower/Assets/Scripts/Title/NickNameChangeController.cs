﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{ 
    public class NickNameChangeController : MonoBehaviour
    {
        [SerializeField] NickNameController controller = null;
        [SerializeField] GameObject nicknamePanel = null;
        [SerializeField] Button okButton = null;
        [SerializeField] InputField inputField = null;
        [SerializeField] Text warnText = null;
        [SerializeField] Button changeNickname = null;
        [SerializeField] Text nicknameDisplay = null;
    // Start is called before the first frame update
        void Start()
        {
            warnText.gameObject.SetActive(false);
            changeNickname.onClick.AddListener(NicknameChangeOpen);
            okButton.onClick.AddListener(OkButtonClicked);
            nicknameDisplay.GetComponent<Text>().text = UserData.nickName;
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
        public void NicknameChangeOpen()
        {
            if (nicknamePanel.activeSelf == true)
                nicknamePanel.gameObject.SetActive(false);
            else
                nicknamePanel.gameObject.SetActive(true);
        }
        private void OkButtonClicked()
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                warnText.gameObject.SetActive(true);
                return;
            }
            controller.UpdateUserInfo(inputField.text);
            nicknameDisplay.GetComponent<Text>().text = UserData.nickName;
        }
    }
}