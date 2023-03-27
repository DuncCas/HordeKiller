using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationDoors : MonoBehaviour
{
    public void OnLevelSelectionDoor()
    {
        GetComponent<Animator>().SetBool("RunOnLevelSelectionDoor", true);
    }
}
