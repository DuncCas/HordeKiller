using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour, ISpawnable {
    [SerializeField]
    int prefId = 0;
    public List<Enemy> enemyTypeSmall;
    public List<Enemy> enemyTypeBig;
    public List<GameObject> EnemyPrefab;
    public List<GameObject> EnemyOnSite; //List.findWithLambda? da cercare
    public float DistanceOffset;
    public long MaxSpawnedSmall=1;
    public long MaxSpawnedBig=1;
    public long SpawnedSmall;
    public long SpawnedBig;
    public long hp_Enemy;
    public bool active;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnedSmall = 0;
        SpawnedBig = 0;
        Spawn(new Vector3(0,0,0));

    }

    public void Spawn(Vector3 pos) { //Questo per spawn iniziale nemici
        Debug.Log("InitEnemies");
        if (active) {
           
            foreach (Enemy en in enemyTypeSmall) {
                while (SpawnedSmall < MaxSpawnedSmall) {
                    Vector3 newPosition = newLocation();
                    SpawnedSmall++;
                    GameObject enemy = Instantiate(EnemyPrefab[prefId], newPosition * DistanceOffset, transform.rotation);
                    EnemyPrefab[prefId].GetComponent<EnemyBehaviour>().en = en;
                    EnemyOnSite.Add(EnemyPrefab[prefId]);
                    //Debug.Log("Spawned");
                }
                Debug.Log("Spawned " + SpawnedSmall + " Enemies");
            }
            prefId++;
            foreach (Enemy en in enemyTypeBig) {
                while (SpawnedBig < MaxSpawnedBig) {
                    Vector3 newPosition = newLocation();
                    SpawnedBig++;
                    GameObject enemy = Instantiate(EnemyPrefab[prefId], newPosition * DistanceOffset, transform.rotation);
                    EnemyPrefab[prefId].GetComponent<EnemyBehaviour>().en = en;
                    EnemyOnSite.Add(EnemyPrefab[prefId]);
                    //Debug.Log("Spawned");
                }
                Debug.Log("Spawned " + SpawnedBig + " Enemies");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Questo serve per ottenere una nuova posizione dove far spawnare nemico mor
    public Vector3 newLocation() {
       RaycastHit2D groundHit;
        
        Vector3 newPosition = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
        //Collider[] nearObstacle = Physics.OverlapBox(newPosition, EnemyPrefab.transform.localScale / 2, Quaternion.identity, 1 << 9);
        //groundHit = Physics2D.Raycast(newPosition, Vector3.down, 1, 1 << 9);
        Debug.DrawRay(newPosition, Vector3.up, Color.blue, 1.0f);
        /*int i = 0;
        if (nearObstacle.Length>0) {
            while (i < nearObstacle.Length-1) {
                Debug.Log(nearObstacle[i].tag);
                i++;
                    }
            return newLocation();
        } else {*/
            
            return newPosition;
        }


    public void depopulate() {
        foreach (GameObject en in EnemyOnSite) {
            en.SetActive(false);
        }
        active = false;
    }

        //if (!groundHit) {
       // Debug.Log("Ground on spawn. Respawning");
            
    }


