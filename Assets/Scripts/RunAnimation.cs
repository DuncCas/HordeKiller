using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimation : MonoBehaviour
{
    public void OnCharacterSelection()
    {
        GetComponent<Animator>().SetBool("RunOnCharacterSelection", true);
    }
    public void OnLevelSelection()
    {
        GetComponent<Animator>().SetBool("RunOnLevelSelection", true);
    }
}
