using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Civilian_Behaviour : MonoBehaviour, ISpawnable, IRandomNumberGenerator
{
    public float SetTimer;
    private float TimeLeft;
    public bool CivilianActive = false;
    float minDistance;
    float maxDistance;
    public float raycastDistance = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;
    //public TextAlignment TimerTxt;

    public TextMeshProUGUI civilianDeadText;
    
    




    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = SetTimer;
        CivilianActive = true;
        minDistance = GameLogic.instance.minRangeToSpawnCiv;
        maxDistance = GameLogic.instance.rangeToSpawnCiv;
        civilianDeadText = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CivilianActive) {
            if(TimeLeft > 0) {
                TimeLeft -= Time.deltaTime;
            } else {
                Debug.Log("Civilians are dead, spawning new");
                gameObject.SetActive(false);
                TimeLeft = 0;
                CivilianActive = false;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Spawn(player.transform.position);
                civilianDeadText.text += "X";
            }
        }
        
    }

    public Vector3 newLocation() {
        RaycastHit hit;
        Vector3 newPosition = new Vector3(-GenerateRandomValue(minDistance, maxDistance)+transform.position.x, transform.position.y, -GenerateRandomValue(minDistance, maxDistance)+transform.position.z);
        if (Physics.Raycast(newPosition, Vector3.down, out hit, raycastDistance)) {
            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, transform.rotation, spawnedObjectLayer);
            Debug.Log("number of colliders found " + numberOfCollidersFound);

            if (numberOfCollidersFound == 0) {
                Debug.Log("spawned civilian");
                return hit.point;
            } else {
                Debug.Log("name of collider 0 found " + collidersInsideOverlapBox[0].name);
                return newLocation();
            }
        }
        return newLocation();
        /*Collider[] intersecting = Physics.OverlapSphere(newPosition, 0.01f);
        if (intersecting.Length != 0) {
            SpawnEnemy();        
        }*/
    }

    public void Spawn(Vector3 pos) { //UGUALE IDENTICO A GETNEWPOSITION IN ENEMYSPAWNER, DA CREARE INTERFACCIA
        Debug.Log("Moving");
        gameObject.transform.position = newLocation(); // da cambiare usa solo val pos.
        gameObject.SetActive(true);
        TimeLeft = SetTimer;
        CivilianActive = true;
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") { //SE TI TOCCA IL PLAYER..
            PlayerHandling player = collision.gameObject.GetComponent<PlayerHandling>();
            //INCREMENTA ARMATURA...
            player.IncreaseArmor();
            CivilianActive = false;
            gameObject.SetActive(false);
            // E SPOSTA Il NUOVO GRUPPO DI CIVILI
            if (GameLogic.instance.state == GameLogic.GameState.PHASE1) {
                Spawn(Vector3.zero);
            }
        }
    }

    public float GenerateRandomValue(float min, float max) {
        float tmp = Random.Range(min, max + 1);
        return tmp;
    }


    /*void updateTimer(float currentTime) {
        currentTime += 1;
    }*/

}

