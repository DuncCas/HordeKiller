using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience_bar : MonoBehaviour
{

    public Image expImage;
    public PlayerHandling player; 

    public void SetMaxExp() {
        //expImage.fillAmount = (float)maxExp;
        expImage.fillAmount = 0;
    }

    public void SetExp(float exp) {
        expImage.fillAmount= (float)exp;
        if (exp >= player.MaxExp)
        {
            SetMaxExp();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
