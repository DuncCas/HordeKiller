using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourBig : EnemyBehaviour
{
   //int prefId = 1;
  override public void Death() {
        hp = 0;
        //Drop exp al 50%
        if (Random.Range(0, 3) < 2) {
            GameLogic.instance.SpawnExp(transform.position);
        }
        //Ricolloco oggetto
        gameObject.SetActive(false);
        EnemySpawner Spawner = player.GetComponent<EnemySpawner>();
        transform.position=Spawner.newLocation(1);
        hp = en.hp;
        gameObject.SetActive(true);

    }
}
