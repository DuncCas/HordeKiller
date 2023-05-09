using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject victoryScreen;
    int armor;
    
    // Start is called before the first frame update
    void Start()
    {
        armor = PlayerHandling.instance.Armor;
    }

    // Update is called once per frame
    void Update()
    {
        armor = PlayerHandling.instance.Armor;

        if (PlayerHandling.instance.Armor >= 2)
        {
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
