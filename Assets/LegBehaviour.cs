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


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerHandling>().Squashed();
        } else if(collision.gameObject.tag== "Enemy") {
            collision.gameObject.GetComponent<EnemyBehaviour>().Squashed();
        }
    }



    // Update is called once per frame



}
