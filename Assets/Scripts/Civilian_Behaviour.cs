using System.Collections;
using System.Collections.Generic;
using HordeKiller;
using UnityEngine;

public class Civilian_Behaviour : MonoBehaviour, ISpawnable
{
    public float SetTimer;
    private float TimeLeft;
    public bool CivilianActive = false;
    //public TextAlignment TimerTxt;






    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = SetTimer;
        CivilianActive = true;
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
                GameObject plyer = GameObject.FindGameObjectWithTag("Player");
                Spawn(plyer.transform.position);
            }
        }
        
    }

    public Vector3 newLocation() {

        Vector3 newPosition = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f));
        return newPosition;
        /*Collider[] intersecting = Physics.OverlapSphere(newPosition, 0.01f);
        if (intersecting.Length != 0) {
            SpawnEnemy();        
        }*/
    }

    public void Spawn(Vector3 pos) { //UGUALE IDENTICO A GETNEWPOSITION IN ENEMYSPAWNER, DA CREARE INTERFACCIA
        gameObject.transform.position = pos + newLocation(); // da cambiare usa solo val pos.
        gameObject.SetActive(true);
        TimeLeft = SetTimer;
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") { //SE TI TOCCA IL PLAYER..
            PlayerHandling player = collision.gameObject.GetComponent<PlayerHandling>();
            //INCREMENTA ARMATURA...
            player.IncreaseArmor();
            // E SPOSTA Il NUOVO GRUPPO DI CIVILI
            CivilianActive = false;
            gameObject.SetActive(false);
            Spawn(collision.transform.position);
        }
    }


    /*void updateTimer(float currentTime) {
        currentTime += 1;
    }*/

}

