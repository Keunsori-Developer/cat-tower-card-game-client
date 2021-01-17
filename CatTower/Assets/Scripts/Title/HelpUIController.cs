using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTower
{
    public class HelpUIController : MonoBehaviour
    {
        [SerializeField] GameObject helpPanel = null;
        [SerializeField] Button previousPage = null;
        [SerializeField] Button nextPage = null;
        // Start is called before the first frame update
        int page;
        void Start()
        {
            page = 1;
            helpPanel.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void NextPage()
        {
            page++;
            if(page>1)
            {
                previousPage.gameObject.SetActive(true);
            }
            if(page==3)
            {
                nextPage.gameObject.SetActive(false);
            }
        }
        public void PreviousPage()
        {
            page--;
            if(page<3)
            {
                nextPage.gameObject.SetActive(true);
            }
            if (page == 1)
            {
                previousPage.gameObject.SetActive(false);
            }
        }
    }
}