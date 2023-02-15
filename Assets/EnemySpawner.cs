using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    //private bool[] ActiveEnemies;
    public float DistanceOffset;
    public long MaxSpawned;
    public long Spawned;
    public bool active;


    // Start is called before the first frame update
    void Start()
    {
        Spawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            if (Spawned < MaxSpawned) {
                SpawnEnemy();
                Spawned++;
            }
        }
    }


    public void SpawnEnemy() {
        Vector3 newPosition = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
        /*Collider[] intersecting = Physics.OverlapSphere(newPosition, 0.01f);
        if (intersecting.Length != 0) {
            SpawnEnemy();
        } else {*/
            GameObject enemy = Instantiate(EnemyPrefab, newPosition*DistanceOffset, transform.rotation);
        //}
     }

    public void OnDeathEnemy() { 
        Spawned--;
    }

}
