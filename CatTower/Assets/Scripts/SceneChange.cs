using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public void ChangeLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void ChangeIngame()
    {
        SceneManager.LoadScene("Ingame_Example");
    }
    public void ChangeRoomSelect()
    {
        SceneManager.LoadScene("Room_Select_Example");
    }
    public void ChangeSetting()
    {
        SceneManager.LoadScene("Setting");
    }

}

  
