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
        [SerializeField] GameObject helpPage1 = null;
        [SerializeField] GameObject helpPage2 = null;
        [SerializeField] GameObject helpPage3 = null;
        // Start is called before the first frame update
        int page;
        void Start()
        {
            helpPanel.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void NextPage()
        {
            page++;
            HelpPageControl();
        }
        public void PreviousPage()
        {
            page--;
            HelpPageControl();
        }
        public void OpenHelpPage()
        {
            page = 1;
            helpPanel.gameObject.SetActive(true);
            HelpPageControl();
        }
        public void HelpPageControl()
        {
            if (page == 1)
            {
                helpPage1.gameObject.SetActive(true);
                helpPage2.gameObject.SetActive(false);
                helpPage3.gameObject.SetActive(false);
                previousPage.gameObject.SetActive(false);
                nextPage.gameObject.SetActive(true);

            }
            if (page == 2)
            {
                helpPage1.gameObject.SetActive(false);
                helpPage2.gameObject.SetActive(true);
                helpPage3.gameObject.SetActive(false);
                previousPage.gameObject.SetActive(true);
                nextPage.gameObject.SetActive(true);
            }
            if (page == 3)
            {
                helpPage1.gameObject.SetActive(false);
                helpPage2.gameObject.SetActive(false);
                helpPage3.gameObject.SetActive(true);
                previousPage.gameObject.SetActive(true);
                nextPage.gameObject.SetActive(false);
            }
        }
    }
}