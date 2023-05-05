using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience_bar : MonoBehaviour
{
    [Tooltip("The experience bar that will fill the frame")]
    public Image expImage;
    PlayerHandling player; 

    public void SetMaxExp() {
        //expImage.fillAmount = (float)maxExp;
        Debug.Log("Resetting");
        expImage.fillAmount = 0;
    }

    public void SetExp(float exp) {
            expImage.fillAmount = (float)exp;
    }

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>();
    }

}
