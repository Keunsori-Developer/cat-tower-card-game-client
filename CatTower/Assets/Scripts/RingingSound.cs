using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingingSound : MonoBehaviour
{
    [SerializeField] Button button;
    AudioClip buttonAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        if (button == null) button = this.gameObject.GetComponent<Button>();

        buttonAudioClip = Resources.Load("Sound/ppyong") as AudioClip;
        // audioSource = this.gameObject.AddComponent<AudioSource>();
        // audioSource.clip = buttonAudioClip;
        button.onClick.AddListener(RingingButtonSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RingingButtonSound()
    {
        GameObject gameObject = new GameObject();
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonAudioClip;
        audioSource.playOnAwake = false;
        audioSource.Play();
        Destroy(gameObject, 1.0f);
    }
}
