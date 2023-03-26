using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickEnterGameplay : MonoBehaviour
{
    public void DelaySceneSwitch() {
        Invoke("SceneSwitcher", 1f);
    }
    public void SceneSwitcher() 
    {
        SceneManager.LoadScene(1);
    }
}
