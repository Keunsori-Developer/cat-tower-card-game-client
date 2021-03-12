using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorHandler : MonoBehaviour
{
    [SerializeField] GameObject[] loadingImages;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        ShowInitialImage();
        StartCoroutine(ShowImages());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowImages()
    {
        while (true)
        {
            loadingImages[count].SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            count++;
            if (count == 4) ShowInitialImage();
        }
    }

    void ShowInitialImage()
    {
        count = 1;
        for (int i = count; i < 4; i++)
        {
            loadingImages[i].SetActive(false);
        }
    }
}
