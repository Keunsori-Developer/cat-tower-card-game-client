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
        [SerializeField] Button openPanel = null;
        [SerializeField] Button closePanel = null;
        // Start is called before the first frame update
        int page;
        int firstPage = 0;
        int lastPage = 2;
        void Start()
        {
            openPanel.onClick.AddListener(OpenHelpPage);
            previousPage.onClick.AddListener(PreviousPage);
            nextPage.onClick.AddListener(NextPage);
            closePanel.onClick.AddListener(CloseHelpPage);
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
        public void CloseHelpPage()
        {
            helpPanel.gameObject.SetActive(false);
        }
        public void OpenHelpPage()
        {
            page = 0;
            helpPanel.gameObject.SetActive(true);
            HelpPageControl();
        }
        public void HelpPageControl()
        {
            
            if (page == firstPage)
            {
                helpPage1.gameObject.SetActive(true);
                helpPage2.gameObject.SetActive(false);
                helpPage3.gameObject.SetActive(false);
                previousPage.gameObject.SetActive(false);
                nextPage.gameObject.SetActive(true);

            }
            else if (page == lastPage)
            {
                helpPage1.gameObject.SetActive(false);
                helpPage2.gameObject.SetActive(false);
                helpPage3.gameObject.SetActive(true);
                previousPage.gameObject.SetActive(true);
                nextPage.gameObject.SetActive(false);
            }
            else
            {
                helpPage1.gameObject.SetActive(false);
                helpPage2.gameObject.SetActive(true);
                helpPage3.gameObject.SetActive(false);
                previousPage.gameObject.SetActive(true);
                nextPage.gameObject.SetActive(true);
            }
        }
    }
}