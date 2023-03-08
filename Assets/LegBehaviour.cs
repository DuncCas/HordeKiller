using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBehaviour : MonoBehaviour
{
    public bool IsOnGround;
    public bool movingUp;
    public RaycastHit underFoot;


    // Start is called before the first frame update
    void Start()
    {
        movingUp = false;
        IsOnGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
