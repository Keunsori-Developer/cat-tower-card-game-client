using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManagers : MonoBehaviour
{
    static InGameManagers _Instance;
    public static InGameManagers GetInstance() { Init(); return _Instance; }
    public static int[] arrSlotIndex;
    public static string[] arrSlotBreed;
    void Start()
    {
        Init();
        arrSlotIndex = new int[57];
        arrSlotBreed = new string[57];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init()
    {
        if (_Instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<InGameManagers>();
            }
            _Instance = go.GetComponent<InGameManagers>();
        }
    }
}
