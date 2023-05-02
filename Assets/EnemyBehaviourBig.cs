using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourBig : EnemyBehaviour
{
    public int prefId = 1;
    // Start is called before the first frame update
 override public void Death() {
        en.hp = 0;
        //Drop exp al 50%
        if (Random.Range(0, 3) < 2) {
            GameLogic.instance.SpawnExp(transform.position);
        }
        //Ricolloco oggetto
        gameObject.SetActive(false);
        EnemySpawner Spawner = player.GetComponent<EnemySpawner>();
        //Vector3 newPosition =
        transform.position=Spawner.newLocation(prefId);
        //transform.position = newPosition;
        gameObject.SetActive(true);
    }
}
