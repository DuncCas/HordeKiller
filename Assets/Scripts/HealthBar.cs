using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    PlayerHandling playerHp;
    Vector3 canvaPosition;
    Vector3 deltaDistance;
    Vector3 currentDeltaDistance;
    Slider slider;

    private void Awake()
    { // deltaDistance è la differenza tra le posizioni inizali del player e del canva in cui c'è la barra della vita
        deltaDistance = transform.position - player.transform.position;
        canvaPosition = transform.position;
    }
    private void Start()
    {
        slider = this.GetComponentInChildren<Slider>();
        playerHp = player.GetComponent<PlayerHandling>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    
    void UpdateHealthBar()
    {
        currentDeltaDistance = transform.position - player.transform.position;
        if (deltaDistance != currentDeltaDistance)
        {   // Aggiorno la posizione del canva in cui c'è la barra della vita
            canvaPosition.x = player.transform.position.x + deltaDistance.x;
            canvaPosition.z = player.transform.position.z + deltaDistance.z;
            transform.position = canvaPosition;
            // Aggiorno il fill della barra della vita 
            slider.value = playerHp.GetHealth();
        }
    }
}
