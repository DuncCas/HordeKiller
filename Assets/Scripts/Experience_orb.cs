using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Experience_orb : MonoBehaviour {


   public GameObject player;

    public float maxTimeToCheck = 10f;
    float TimeToCheck;
    public float maxDistanceFromPlayer;

    void Start() {
        player= GameObject.FindGameObjectWithTag("Player");
        TimeToCheck = 0;
    }


    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player.GetComponent<PlayerHandling>().IncreaseExp();
            gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (gameObject.activeInHierarchy) {
            if (TimeToCheck >= maxTimeToCheck) {
                checkDistance();
                TimeToCheck = 0;
            }
            TimeToCheck += Time.deltaTime;
        }
    }

    private void checkDistance() {
        float distance= Vector3.Distance(transform.position, player.transform.position);
        if (distance>= maxDistanceFromPlayer) {
            gameObject.SetActive(false);
        }
    }
}

