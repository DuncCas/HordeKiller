using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience_orb : MonoBehaviour {

   public PlayerHandling player;
    // Start is called before the first frame update



    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player.IncreaseExp();
            gameObject.SetActive(false);
        }
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}

