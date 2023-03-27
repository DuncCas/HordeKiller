using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawnable {
    [SerializeField]
    public List<Enemy> enemyTypes;
    public GameObject EnemyPrefab;
    public List<GameObject> EnemyOnSite; //List.findWithLambda? da cercare
    public float DistanceOffset;
    public long MaxSpawned;
    public long Spawned;
    public long hp_Enemy;
    public bool active;


    // Start is called before the first frame update
    void Awake()
    {
        Spawned = 0;
        Spawn(new Vector3(0,0,0));

    }

    public void Spawn(Vector3 pos) { //Questo per spawn iniziale nemici
        Debug.Log("InitEnemies");
        if (active) {
            foreach (Enemy en in enemyTypes) {
                while (Spawned < MaxSpawned) {
                    Vector3 newPosition = newLocation();
                    GameObject enemy = Instantiate(EnemyPrefab, newPosition * DistanceOffset, transform.rotation);
                    EnemyPrefab.GetComponent<EnemyBehaviour>().en = en;
                    EnemyOnSite.Add(EnemyPrefab);
                    Debug.Log("Spawned");
                }
                Debug.Log("Spawned " + Spawned + " Enemies");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Questo serve per ottenere una nuova posizione dove far spawnare nemico mor
    public Vector3 newLocation() { 

        Vector3 newPosition = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
        Spawned++;
        return newPosition;
    }


}
