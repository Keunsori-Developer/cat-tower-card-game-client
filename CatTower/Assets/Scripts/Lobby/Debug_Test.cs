using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Test : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(Wait_Debug());
        
    }

    IEnumerator Wait_Debug() 
    {
        for (int i = 0; i< 100; i++){
            Debug.Log("Test");
            yield return new WaitForSeconds(5);
        }
        
    }
   
}
