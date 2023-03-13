using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBehaviour : MonoBehaviour {
    public enum LEG_STATUS {
        IDLE,
        ARRIVING,
        GROUND,
        LEAVING
    }
    private bool OnGround;
    
    public RaycastHit underFoot;
    private float distToGround;
    private float fallingSpeed;
    private float raisingSpeed;

    // Start is called before the first frame update
    void Start() {
        OnGround = false;
    }

    //Controlla se è a terra
    public bool IsOnGround() {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }

  

    


    // Update is called once per frame
    

    
}
