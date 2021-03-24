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
        [SerializeField] GameObject helpPage4 = null;
        [SerializeField] GameObject helpPage5 = null;
        [SerializeField] GameObject helpPage6 = null;
        [SerializeField] GameObject helpPage7 = null;
        [SerializeField] Text currentPage = null;
        [SerializeField] Button openPanel = null;
        [SerializeField] Button closePanel = null;
        // Start is called before the first frame update
        int page;
        int firstPage = 0;
        int lastPage = 6;
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
            helpPage1.gameObject.SetActive(false);
            helpPage2.gameObject.SetActive(false);
            helpPage3.gameObject.SetActive(false);
            helpPage4.gameObject.SetActive(false);
            helpPage5.gameObject.SetActive(false);
            helpPage6.gameObject.SetActive(false);
            helpPage7.gameObject.SetActive(false);
            HelpPageControl();
            UpdatePage(page + 1);

        }
        public void HelpPageControl()
        {
                helpPage1.gameObject.SetActive(false);
                helpPage2.gameObject.SetActive(false);
                helpPage3.gameObject.SetActive(false);
                helpPage4.gameObject.SetActive(false);
                helpPage5.gameObject.SetActive(false);
                helpPage6.gameObject.SetActive(false);
                helpPage7.gameObject.SetActive(false);
                switch (page+1)
                {
                    case 1:
                        helpPage2.gameObject.SetActive(true);
                        break;
                    case 2:
                        helpPage2.gameObject.SetActive(true);
                        break;
                    case 3:
                        helpPage3.gameObject.SetActive(true);
                        break;
                    case 4:
                        helpPage4.gameObject.SetActive(true);
                        break;
                    case 5:
                        helpPage5.gameObject.SetActive(true);
                        break;
                    case 6:
                        helpPage6.gameObject.SetActive(true);
                        break;
                     case 7:
                        helpPage7.gameObject.SetActive(true);
                        break;
            }
            if (page == firstPage)
            {
                previousPage.gameObject.SetActive(false);
                nextPage.gameObject.SetActive(true);

            }
            else if (page == lastPage)
            {
                previousPage.gameObject.SetActive(true);
                nextPage.gameObject.SetActive(false);
            }
            else
            {
                previousPage.gameObject.SetActive(true);
                nextPage.gameObject.SetActive(true);
            }
            UpdatePage(page + 1);
        }

        public void UpdatePage(int n)
        {
            currentPage.GetComponent<Text>().text = n.ToString();
        }
    }
}