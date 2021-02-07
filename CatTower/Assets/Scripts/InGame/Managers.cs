using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _Instance;
    public static Managers GetInstance() { Init(); return _Instance; }
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
                go.AddComponent<Managers>();
            }
            _Instance = go.GetComponent<Managers>();
        }
    }
}
