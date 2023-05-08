using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Set Time Of Life of projectile")]
    public int Lifetime = 4;
[Header("Projectile Sounds")]
    public AudioClip[] shootSound;
    public AudioClip[] shootLandingEnemy;
    public AudioClip[] shootLandingWall;
    PlayerHandling _pltmp;

   

    void Start()
    {
        _pltmp=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>();

    }

    private void OnEnable() {
        //AudioSource.PlayClipAtPoint(pickShootSound(shootSound), transform.position, 0.5f);
        StartCoroutine(DestroyOverLifetime(Lifetime));
    }


    private void OnDisable() {
        StopCoroutine(DestroyOverLifetime(Lifetime));
    }




    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            /*if (other.tag == "Enemy") {
                AudioSource.PlayClipAtPoint(pickShootSound(shootLandingEnemy), transform.position, 0.5f);
            } else {
                AudioSource.PlayClipAtPoint(pickShootSound(shootLandingWall), transform.position, 0.5f);
            }*/
            gameObject.SetActive(false);
        }
    }

 IEnumerator DestroyOverLifetime(int lifetime) {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
    private AudioClip pickShootSound(AudioClip[] type) {
        return type[(int)UnityEngine.Random.Range(0, type.Length)];
    }

}



