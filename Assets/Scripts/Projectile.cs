using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    public Material baseMat;
    // Start is called before the first frame update
   

    void Start()
    {
    }

    // Update is called once per frame

    private void Update() {
   // mettere un set active qua dopo tot frame
    }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            gameObject.SetActive(false);
        }
    }
}



