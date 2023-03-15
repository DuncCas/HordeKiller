using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    public GameObject Civilian;

    // Start is called before the first frame update
    void Start() {
        Instantiate(Civilian, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)), transform.rotation);
    }

    // Update is called once per frame
    void Update() {

    }
}
