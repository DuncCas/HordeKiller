using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    // Script provvisorio, da mettere successivamente in GameLogic

    private PlayerHandling player;
    public GameObject GameOverMenu;
    public Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDeath();
    }

    public void RevivePlayer()
    {
        player.GetComponent<PlayerHandling>().revive();
        Time.timeScale = 1;
        
    }

    public void PlayerDeath()
    {
        if(GameLogic.instance.getGameState() != GameLogic.GameState.START)
        {
            if (player.GetHealth() <= 0)
            {
                Time.timeScale = 0;
                GameOverMenu.SetActive(true);
                pauseButton.interactable = false;
            }
        }  
    }
}
