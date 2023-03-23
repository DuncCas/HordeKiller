using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float Lifetime = 4f;
    //public Material baseMat;
    public PlayerHandling pltmp;
    // Start is called before the first frame update
   

    void Start()
    {
        StartCoroutine(DestroyOverLifetime(Lifetime));
    }

    // Update is called once per frame

    private void Update() {
       
    }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            gameObject.SetActive(false);
            pltmp._projectilePool.Add(this);
        }
    }

    private IEnumerator DestroyOverLifetime(float lifetime) {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
        pltmp._projectilePool.Add(this);
    }
}



