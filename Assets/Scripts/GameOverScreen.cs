using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    // Script provvisorio, da mettere successivamente in GameLogic

    public GameObject player;
    private float playerHp;
    public GameObject GameOverMenu;
    public Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHp = player.GetComponent<PlayerHandling>().GetHealth();
        PlayerDeath();
    }

    public void RevivePlayer()
    {
        player.GetComponent<PlayerHandling>().revive();
        Time.timeScale = 1;
        
    }

    public void PlayerDeath()
    {
        if (playerHp <= 0)
        {
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
            pauseButton.interactable = false;
        }
    }
}
