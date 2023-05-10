using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
       
        Invoke("MuteIntro", 4.2f);
        Invoke("LoadGameplayFromIntro", 6f);
    }

    public void LoadGameplayFromIntro()
    {
        SceneManager.LoadScene(1);
    }

    public void MuteIntro()
    {
        audio.volume = 0f;
    }
}
