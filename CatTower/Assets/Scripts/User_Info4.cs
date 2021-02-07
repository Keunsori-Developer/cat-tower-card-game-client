using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_Info4 : MonoBehaviour
{
    public Text user;
    int count = 0;
    void Start()
    {

        StartCoroutine(Wait_Debug());


    }

    IEnumerator Wait_Debug()
    {

        yield return new WaitForSeconds(20);
        user.text = "Jinsu";


    }
    void Update()
    {

    }
}
