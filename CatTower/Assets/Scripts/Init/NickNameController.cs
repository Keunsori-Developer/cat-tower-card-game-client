using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTower
{
    public class NickNameController : MonoBehaviour
    {
        [SerializeField] GameObject nicknameCanvas = null;

        private void Awake()
        {
            nicknameCanvas.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenInputUI()
        {
            nicknameCanvas.SetActive(true);
        }

        public void UpdateUserInfo(string nickname)
        {
            UserData.nickName = nickname;
            Debug.Log(string.Format("mid: {0}, nickname: {1}", UserData.mid, UserData.nickName));
            var initializer = this.gameObject.GetComponent<GameInitializer>();
            if (initializer != null) initializer.message.Send();
            nicknameCanvas.SetActive(false);
        }
    }
}