using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_Info2 : MonoBehaviour
{
    public Text user;
    int count = 0;
    void Start()
    {

        StartCoroutine(Wait_Debug());


    }

    IEnumerator Wait_Debug()
    {

        yield return new WaitForSeconds(10);
        user.text = "SeungGom";


    }
    void Update()
    {

    }
}
