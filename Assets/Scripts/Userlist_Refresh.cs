/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CatTower{
public class Userlist_Refresh : MonoBehaviour
{
    public Text nickname;
    public Text mid;

    List<string> user = new List<string>();

    // Use this for initialization
    void Start()
    {
        WebSocketManager.socket.On(" ", (data) =>
        {
            Debug.Log(data.Json.args[0]);
            buffer.Add(data.Json.args[0].ToString());
        });
    }

    void Update()
    {
        if (user.Count <= 0) return;

        foreach (var b in user)
        {
            mid.text += b + "\n";
        }
        user.Clear();
        }
    
    }
}*/