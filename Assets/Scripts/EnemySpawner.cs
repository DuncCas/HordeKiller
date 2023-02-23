using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
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
        InitEnemies();

    }

    private void InitEnemies() { //Questo per spawn iniziale nemici
        Debug.Log("InitEnemies");
        if (active) {
            while (Spawned < MaxSpawned) {
                Vector3 newPosition = GetNewPosition();
                GameObject enemy = Instantiate(EnemyPrefab, newPosition * DistanceOffset, transform.rotation);
                //}
                EnemyOnSite.Add(EnemyPrefab);
                Debug.Log("Spawned");
            }
            Debug.Log("Spawned "+Spawned+" Enemies");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Questo serve per ottenere una nuova posizione dove far spawnare nemico mor
    public Vector3 GetNewPosition() { 

        Vector3 newPosition = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
        Spawned++;
        return newPosition;
        /*Collider[] intersecting = Physics.OverlapSphere(newPosition, 0.01f);
        if (intersecting.Length != 0) {
            SpawnEnemy();        
        }*/
    }

    /*public void OnDeathEnemy(GameObject Enemy) {
        if (Enemy) {
            Debug.Log("Enemy dead, relocating");
            Spawned--;
            GameObject ToRelocate = EnemyOnSite.Find(e => e==Enemy);
            SpawnEnemy(ToRelocate);
        }
    }*/

}
