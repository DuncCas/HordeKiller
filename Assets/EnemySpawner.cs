using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    public float DistanceOffset = 5f;
    public long MaxSpawned;
    private long Spawned;
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
        Vector3 newPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        GameObject enemy = Instantiate(EnemyPrefab, newPosition, transform.rotation);
    }

    public void OnDeathEnemy() { 
        Spawned--;
    }

}
