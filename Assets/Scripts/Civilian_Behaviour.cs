using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Civilian_Behaviour : MonoBehaviour, ISpawnable, IRandomNumberGenerator
{
    [Tooltip("Set the timer when civilian 'dies'")]
    public float SetTimer;
    [Tooltip("Define all the layer mask where it's ok to spawn in. Default= ground")]
    public LayerMask spawnedObjectLayer;
    float TimeLeft;
    bool CivilianActive = false;
    float minDistance;
    float maxDistance;
    const float RAYCAST_DISTANCE = 100f;
    const float OVERLAP_BOX_SIZE = 1f;
    //public TextAlignment TimerTxt;
    [Tooltip("UI text for keeping track of dead civilians")]
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

    #region SPAWN
    public Vector3 newLocation() {
        RaycastHit hit;
        Vector3 newPosition = new Vector3(-GenerateRandomValue(minDistance, maxDistance)+transform.position.x, transform.position.y, -GenerateRandomValue(minDistance, maxDistance)+transform.position.z);
        if (Physics.Raycast(newPosition, Vector3.down, out hit, RAYCAST_DISTANCE)) {
            Vector3 overlapTestBoxScale = new Vector3(OVERLAP_BOX_SIZE, OVERLAP_BOX_SIZE, OVERLAP_BOX_SIZE);
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
    }

    public void Spawn(Vector3 pos) { 
        Debug.Log("Moving");
        gameObject.transform.position = newLocation(); 
        gameObject.SetActive(true);
        TimeLeft = SetTimer;
        CivilianActive = true;
    }
    public float GenerateRandomValue(float min, float max) {
        float tmp = Random.Range(min, max + 1);
        return tmp;
    }
    #endregion

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") { //SE TI TOCCA IL PLAYER..
            PlayerHandling player = collision.gameObject.GetComponent<PlayerHandling>();
            //INCREMENTA ARMATURA...
            player.IncreaseArmor();
            CivilianActive = false;
            gameObject.SetActive(false);
            // E SPOSTA Il NUOVO GRUPPO DI CIVILI
            if (GameLogic.instance.getGameState() == GameLogic.GameState.PHASE1) {
                Spawn(Vector3.zero);
            }
        }
    }

}
