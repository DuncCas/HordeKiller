using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayButton : MonoBehaviour
{
    public void Delay() {
        Invoke("SetActiveButton", 2f);
    }
    
    public void SetActiveButton() {
        this.gameObject.SetActive(true);
    }
}

