using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject victoryScreen;
    int armor = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void checkForVictory() {
        armor = PlayerHandling.instance.Armor;
        if (PlayerHandling.instance.Armor >= GameLogic.instance.maxArmorToCollect) {
            Time.timeScale = 0f;
            victoryScreen.SetActive(true);
        }
    }


    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(1);
    }
}
