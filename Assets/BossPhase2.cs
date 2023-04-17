using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2 : MonoBehaviour
{
    float maxCooldown = 20f;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate() {
        transform.LookAt(player.transform);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
