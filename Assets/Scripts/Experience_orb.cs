using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Experience_orb : MonoBehaviour {



    [Tooltip("Timer to trigger check of the player distance")]
    public float maxTimeToCheck = 10f;
    [Tooltip("The max distance from player to trigger exp deactivation/repositioning")]
    public float maxDistanceFromPlayer;
    GameObject _player;
    float _TimeToCheck;

    void Start() {
        _player= GameObject.FindGameObjectWithTag("Player");
        _TimeToCheck = 0;
    }

    private void Update() {
        if (gameObject.activeInHierarchy) {
            if (_TimeToCheck >= maxTimeToCheck) {
                checkDistance();
                _TimeToCheck = 0;
            }
            _TimeToCheck += Time.deltaTime;
        }
    }


    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            _player.GetComponent<PlayerHandling>().IncreaseExp();
            gameObject.SetActive(false);
        }
    }


    private void checkDistance() {
        float distance= Vector3.Distance(transform.position, _player.transform.position);
        if (distance>= maxDistanceFromPlayer) {
            gameObject.SetActive(false);
        }
    }
}

