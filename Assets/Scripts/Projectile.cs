using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int Lifetime = 4;
    public PlayerHandling pltmp;

   

    void Start()
    {
        pltmp=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>();

    }

    private void OnEnable() {
        StartCoroutine(DestroyOverLifetime(Lifetime));
    }

    private void OnDisable() {
        StopCoroutine(DestroyOverLifetime(Lifetime));
    }




    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            gameObject.SetActive(false);
        }
    }

 IEnumerator DestroyOverLifetime(int lifetime) {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}



