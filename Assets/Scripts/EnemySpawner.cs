using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour, IRandomNumberGenerator { //ISpawnable {
    [SerializeField]
    [Header("Enemies Prefabs")]
    [Tooltip("Enemies prefab to associate scriptable Objects. NOTE: The order is 0:small, 1:Big")]
    public GameObject[] EnemyPrefab;
    [Tooltip("Assign the number of enemies to spawn for each prefab type. 0: Number of small enemies to spawn, 1:Number of big enemies to spawn")]
    public int[] maxSpawned;
    [Tooltip("Assign the scriptable object to assign to enemy prefabs. 0: SO for small enemies, 1:SO for big enemies")]
    public Enemy[] EnemyType;
    int[] _EnemyOnSite;
    List<GameObject> _AllEnemiesOnMap;
     //List.findWithLambda? da cercare
    //public float DistanceOffset;

    [Header("Spawn Settings")]
    [Tooltip("Should the spawner be active")]
    public bool active;
    [Tooltip("Max possible distance to spawn")]
    [Range(15f, 100f)]
    public float rangeToSpawn;
    //public GameObject[] itemsToPickFrom;
    [Tooltip("Raycast lenght to check for collision")]
    public float raycastDistance = 100f;
    [Tooltip("Boxcast size")]
    public float overlapTestBoxSize = 1f;
    [Tooltip("Specifies the layers's collisions to ignore when spawning (i.e. Ground)")]
    public LayerMask spawnedObjectLayer;
    const float MIN_RANGE_TO_SPAWN = 15f;

    void Awake()
    {
        _AllEnemiesOnMap = new List<GameObject>();
        InitSpawn();

    }

    private void InitSpawn() { //Questo per spawn iniziale nemici
        Debug.Log("InitEnemies");
            //per ora vi è foreach ma sarebbe figo avere diversi tipi di nemici e pigliarne a random
            for (int i=0; i < EnemyPrefab.Length;i++) {
                for (int j=0; j < maxSpawned[i]; j++){
                    GameObject clone = Instantiate(EnemyPrefab[i], newLocation(i), transform.rotation);
                    EnemyPrefab[i].GetComponent<EnemyBehaviour>().en = EnemyType[i];
                    _EnemyOnSite[i]++;
                    _AllEnemiesOnMap.Add(clone);
            }
                Debug.Log("Spawned " + _EnemyOnSite[i]+ " enemies");
            }
            
        
    }


    //Questo serve per ottenere una nuova posizione dove far spawnare nemico mor
    public Vector3 newLocation(int i) {
            RaycastHit hit;
            Vector3 newPosition = new Vector3(GenerateRandomValue(MIN_RANGE_TO_SPAWN, rangeToSpawn) +transform.position.x, transform.position.y, GenerateRandomValue(MIN_RANGE_TO_SPAWN, rangeToSpawn) + transform.position.z);
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

    public void depopulate() {
        foreach(GameObject i in _AllEnemiesOnMap) {
            i.SetActive(false);
            _AllEnemiesOnMap.Remove(i);
        }
        active = false;
    }

   public float GenerateRandomValue(float min, float max) {
        long neg = (long)Random.Range(0, 2);
        float tmp = Random.Range(min, max + 1);
        if (neg <= 0) {
            return -tmp;
        } else {
            return tmp;
        }
    }

}


