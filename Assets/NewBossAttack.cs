using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossAttack : MonoBehaviour
{
    public SpriteRenderer sprite;
    GameObject player;
    public ParticleSystem explosion;
    float currentTimer=0;
    public Boss data;
    public float killTimeMax;
    public float calloutTime = 4f;
    float currentKillTime=0;
    bool attack;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        Invoke("ShowTarget", data.maxTimeBeforeStomp - calloutTime);
    }


    // Update is called once per frame
    void Update()
    {
       if (!attack) {
       if (currentTimer >= data.maxTimeBeforeStomp) {
            ActivateKillSphere();
            } else {
                currentTimer += Time.deltaTime;
            }
        } else {
            if (currentKillTime >= killTimeMax) {
                DeactivateKillSphere();
            } else {
                currentKillTime += Time.deltaTime;
            }
        }
    }

    private void DeactivateKillSphere() {
        currentKillTime = 0;
        attack = false;
        GetComponent<SphereCollider>().enabled = false;
        sprite.enabled = false;
        Invoke("ShowTarget", data.maxTimeBeforeStomp - calloutTime);
    }

    private void ActivateKillSphere() {
        currentTimer = 0;
        attack = true;
        GetComponent<SphereCollider>().enabled = true;
       

    }

    void ShowTarget() {
        transform.position = player.transform.position;
        sprite.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        other.GetComponent<PlayerHandling>().Squashed();
    }


}
