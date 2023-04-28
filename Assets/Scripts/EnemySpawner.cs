using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour { //ISpawnable {
    [SerializeField]
    //int currentPrefId = 0;
    public GameObject[] EnemyPrefab;
    public int[] EnemyOnSite;
    public Enemy[] EnemyType;
    private List<GameObject> AllEnemiesOnMap;
    public List<Enemy> enemyTypeSmall;
    public List<Enemy> enemyTypeBig;
     //List.findWithLambda? da cercare
    public float DistanceOffset;
    public long MaxSpawnedSmall=1;
    public long MaxSpawnedBig=1;
    public long SpawnedSmall;
    public long SpawnedBig;
    public long hp_Enemy;
    public bool active;
    public GameObject[] itemsToPickFrom;
    public float raycastDistance = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;

    // Start is called before the first frame update
    void Awake()
    {
        AllEnemiesOnMap = new List<GameObject>();
        for(int i=0; i <= EnemyPrefab.Length - 1;i++) {
            EnemyOnSite[i] = 0;
        }
        InitSpawn();

    }

    private void InitSpawn() { //Questo per spawn iniziale nemici
        Debug.Log("InitEnemies");
        if (active) {
        int prefid = 0;
            //per ora vi è foreach ma sarebbe figo avere diversi tipi di nemici e pigliarne a random
            for (int i=0; i <= EnemyPrefab.Length-1;i++) {
                while (EnemyOnSite[i] < MaxSpawnedSmall) {
                    // Vector3 newPosition =
                    newLocation(prefid);
                    

                    /*GameObject enemy = Instantiate(EnemyPrefab[prefId], newPosition * DistanceOffset, transform.rotation);
                    EnemyPrefab[prefId].GetComponent<EnemyBehaviour>().en = en;
                    EnemyOnSite.Add(EnemyPrefab[prefId]);
                    //Debug.Log("Spawned");
                }*/
                    Debug.Log("Spawned " + SpawnedSmall + " Enemies");
                }
        
                /*prefId++;
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
                }*/
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Questo serve per ottenere una nuova posizione dove far spawnare nemico mor
    public void  newLocation(int i) {

        RaycastHit hit;

        Vector3 newPosition = new Vector3(Random.Range(-10f, 10f), transform.position.y, Random.Range(-10f, 10f));
        if (Physics.Raycast(newPosition, Vector3.down, out hit, raycastDistance)) {
            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, transform.rotation, spawnedObjectLayer);
            Debug.Log("number of colliders found " + numberOfCollidersFound);

            if (numberOfCollidersFound == 0) {
                Debug.Log("spawned enemy");
                GameObject clone = Instantiate(EnemyPrefab[i], hit.point, transform.rotation);
                EnemyPrefab[i].GetComponent<EnemyBehaviour>().en = EnemyType[i];
                EnemyOnSite[i]++;
                AllEnemiesOnMap.Add(clone);
            } else {
                Debug.Log("name of collider 0 found " + collidersInsideOverlapBox[0].name);
            }
        }
    }


        /*RaycastHit2D groundHit;

        Vector3 newPosition = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));
        Collider[] nearObstacle = Physics.OverlapBox(newPosition, EnemyPrefab[0].transform.localScale / 2, Quaternion.identity, 1 << 9);
        //groundHit = Physics2D.Raycast(newPosition, Vector3.down, 1, 1 << 9);
        Debug.DrawRay(newPosition, Vector3.up, Color.blue, 1.0f);
        if (nearObstacle != null) {
            Debug.LogWarning("collision, changing location");
            return new Vector3(0,0,0);
        } else {

            return newPosition;
        }
    }*/


    public void depopulate() {
        foreach(GameObject i in AllEnemiesOnMap) {
            Destroy(i);
        }
        active = false;
    }

        //if (!groundHit) {
       // Debug.Log("Ground on spawn. Respawning");
            

public void Spawn(Vector3 pos) { //Questo per spawn iniziale nemici
    Debug.Log("InitEnemies");
    int prefid = 0;
    if (active) {
        foreach (Enemy en in enemyTypeSmall) {
            while (SpawnedSmall < MaxSpawnedSmall) {
                // Vector3 newPosition =
                newLocation(prefid);


                /*GameObject enemy = Instantiate(EnemyPrefab[prefId], newPosition * DistanceOffset, transform.rotation);
                EnemyPrefab[prefId].GetComponent<EnemyBehaviour>().en = en;
                EnemyOnSite.Add(EnemyPrefab[prefId]);
                //Debug.Log("Spawned");
            }*/
                Debug.Log("Spawned " + SpawnedSmall + " Enemies");
            }

            /*prefId++;
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
            }*/
        }
    }
}
}


