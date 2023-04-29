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
     //List.findWithLambda? da cercare
    public float DistanceOffset;
    public int[] maxSpawned;
    public bool active;
    public GameObject[] itemsToPickFrom;
    public float raycastDistance = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;

    // Start is called before the first frame update
    void Awake()
    {
        AllEnemiesOnMap = new List<GameObject>();
        InitSpawn();

    }

    private void InitSpawn() { //Questo per spawn iniziale nemici
        Debug.Log("InitEnemies");

        //int prefid = 0;
            //per ora vi è foreach ma sarebbe figo avere diversi tipi di nemici e pigliarne a random
            for (int i=0; i <= EnemyPrefab.Length;i++) {
                for (int j=0; j < maxSpawned[i]; j++){
                    // Vector3 newPosition =
                    
                    GameObject clone = Instantiate(EnemyPrefab[i], newLocation(i), transform.rotation);
                    EnemyPrefab[i].GetComponent<EnemyBehaviour>().en = EnemyType[i];
                    EnemyOnSite[i]++;
                    AllEnemiesOnMap.Add(clone);

                /*GameObject enemy = Instantiate(EnemyPrefab[prefId], newPosition * DistanceOffset, transform.rotation);
                EnemyPrefab[prefId].GetComponent<EnemyBehaviour>().en = en;
                EnemyOnSite.Add(EnemyPrefab[prefId]);
                //Debug.Log("Spawned");
            }*/
                //Debug.Log("Spawned " + SpawnedSmall + " Enemies");
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
                Debug.Log("Spawned " + EnemyOnSite[i]+ " enemies");
            }
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Questo serve per ottenere una nuova posizione dove far spawnare nemico mor
    public Vector3 newLocation(int i) {
            RaycastHit hit;

            Vector3 newPosition = new Vector3(Random.Range(-10f, 10f), transform.position.y, Random.Range(-10f, 10f));
            if (Physics.Raycast(newPosition, Vector3.down, out hit, raycastDistance)) {
                Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
                Collider[] collidersInsideOverlapBox = new Collider[1];
                int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, transform.rotation, spawnedObjectLayer);
                Debug.Log("number of colliders found " + numberOfCollidersFound);

                if (numberOfCollidersFound == 0) {
                        Debug.Log("spawned enemy");
                        return hit.point;                  
                } else {
                    Debug.Log("name of collider 0 found " + collidersInsideOverlapBox[0].name);
                    return newLocation(i);
                }
            }
            return newLocation(i);
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
            i.SetActive(false);
            AllEnemiesOnMap.Remove(i);
        }
        active = false;
    }

        //if (!groundHit) {
       // Debug.Log("Ground on spawn. Respawning");
            

/*public void Spawn(int i) { //Questo per spawn iniziale nemici
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
                //Debug.Log("Spawned " + SpawnedSmall + " Enemies");
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


