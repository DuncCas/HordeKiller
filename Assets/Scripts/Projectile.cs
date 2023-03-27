using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int Lifetime = 4;
    //public Material baseMat;
    public PlayerHandling pltmp;
    // Start is called before the first frame update
   

    void Start()
    {
        pltmp=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>();
        //StartCoroutine(DestroyOverLifetime(Lifetime));
    }

    private void OnEnable() {
        StartCoroutine(DestroyOverLifetime(Lifetime));
    }

    private void OnDisable() {
        StopCoroutine(DestroyOverLifetime(Lifetime));
    }


    // Update is called once per frame

    private void Update() {
       
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



