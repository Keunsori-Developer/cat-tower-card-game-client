using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatTower
{
    public class GameInitializer : MonoBehaviour
    {

        IEnumerator loader = null;
        public Message message = new Message();

        // Start is called before the first frame update
        void Start()
        {
<<<<<<< Updated upstream
=======
            /*exampleCode
            HttpManager.Instance.Post<RoomInfoRequest, RoomInfoResponse>("/rooms/create", 
            new RoomInfoRequest {
                hostId = "11111",
                name = "방이름"
            }, null);
            */

>>>>>>> Stashed changes
            message.AddListener(() => loader.MoveNext());
            loader = Loader();
            loader.MoveNext();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator Loader()
        {
            if (string.IsNullOrEmpty(UserInfo.mid))
            {
                var controller = this.gameObject.GetComponent<NickNameController>();
                if (controller != null) controller.OpenInputUI();
                yield return null;
            }

            Debug.Log("finish");
            SceneManager.LoadScene("Title");

        }

    }
}