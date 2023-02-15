using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience_bar : MonoBehaviour
{

    public Slider slider;


    public void SetMaxExp(int maxExp) {
        slider.maxValue = (float)maxExp;
        slider.value = 0;
    }

    public void SetExp(int exp) {
        slider.value= (float)exp;
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
