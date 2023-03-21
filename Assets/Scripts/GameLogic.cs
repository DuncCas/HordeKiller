using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    public GameObject civilian;
    public float bossHeight;
    public GameObject boss;
    public static GameLogic instance;
    // Start is called before the first frame update


    private void Awake() {
        instance = this;
        Instantiate(civilian, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)), transform.rotation);
        Instantiate(boss, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * bossHeight, transform.rotation);

    }
    void Start() {
        

    }

    // Update is called once per frame
    void Update() {

    }
}
