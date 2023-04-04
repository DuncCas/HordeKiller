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
            Instantiate(exp, transform.position, transform.rotation);
        }
        //Ricolloco oggetto
        gameObject.SetActive(false);
        EnemySpawner Spawner = player.GetComponent<EnemySpawner>();
        Spawner.SpawnedBig--;
        Vector3 newPosition = Spawner.newLocation();
        transform.position = newPosition;
        en.hp = Spawner.hp_Enemy;
        gameObject.SetActive(true);
    }
}
