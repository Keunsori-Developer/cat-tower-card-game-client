using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User_Info : MonoBehaviour
{
    public Text user;
    int count = 0;
    void Start()
    {
        
            StartCoroutine(Wait_Debug());
        
        
    }

    IEnumerator Wait_Debug()
    {
       
            yield return new WaitForSeconds(5);
            user.text = "Minho";


    }
   void Update()
    {
        
    }
}
