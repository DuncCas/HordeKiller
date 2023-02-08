using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience_orb : MonoBehaviour {

   public GameObject player;
    // Start is called before the first frame update

    void Start() {
        player= GameObject.FindGameObjectWithTag("Player");
    }


    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player.GetComponent<PlayerHandling>().IncreaseExp();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}

