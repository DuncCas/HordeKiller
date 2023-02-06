using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float lifetime = 10f;
    public Material baseMat;
    // Start is called before the first frame update
   

    void Start()
    {
        StartCoroutine(DestroyOverLifetime(lifetime));
    }

    // Update is called once per frame

    private void Update() {
   // mettere un set active qua dopo tot frame
    }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DestroyOverLifetime(float lifetime) {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}



