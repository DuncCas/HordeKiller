using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{

    //PER ORA STATI VANNO QUA MA LI DEVO METTERE IN UNA NUOVA FUNZIONE SRY NOT SRY
  public  enum BOSS_STATES {
        IDLE,
        MOVING,
        SPAWNING
        //ENDFIRSTPHASE
        //ENDGAME

    }

    public GameManager gameManager;
    public PlayerHandling player;

    BOSS_STATES state;
    BOSS_STATES previousState;

    

    public float movementSpeed = 1.5f;
    public float damageOnTouch = 40f;
    public float damageOnStomp; //Magari se vieni schiacciato muori e basta?


    public float timeBeforeBossMoves;
    public float maxtimeBeforeBossMoves;
    public float movementDuration;


    public float timeBetweenNewSpawns; //alcuni nemici partono da lui
    public float maxTimeBeforeNewSpawns;




    // Start is called before the first frame update
    void Start()
    {
        state = BOSS_STATES.IDLE;
        previousState = BOSS_STATES.IDLE;
        timeBetweenNewSpawns = 0;
        timeBeforeBossMoves=0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case BOSS_STATES.IDLE:
                Update_IDLE();
                break;
                
            case BOSS_STATES.MOVING:
                Update_MOVING();
                break;
            case BOSS_STATES.SPAWNING:
                Update_SPAWNING();
                break;


        }
    }


    public void ChangeState(BOSS_STATES newState) {
        if (state != newState) {
            previousState = state;
            state = newState;
        }
    }




    private void Update_IDLE() {
        // RIMANE FERMO, MAGARI METTERE DEI EFFETTI TIPO CIOTTOLI CHE CADONO DALL'ALTO O QUALCOSA DEL GENERE
        // QUANDO FINISCE TIMER MOVIMENTO => MOVING
        if (timeBeforeBossMoves >= maxtimeBeforeBossMoves) {
            ChangeState(BOSS_STATES.MOVING);
        } else if (timeBetweenNewSpawns >= maxTimeBeforeNewSpawns) { // QUANDO FINISCE TIMER SPAWN => SPAWNING
            ChangeState(BOSS_STATES.SPAWNING);
        }
        // SE GIOCATORE OTTIENE TUTTI PEZZI ARMATURA => ENDFIRSTPHASE
        
        // if (gameManager.status== ENDFIRSTPHASE){
        //  ChangeState(BOSS_STATES.ENDFIRSTPHASE)
        //  }
        
        // SE MUORE GIOCATORE => ENDGAME

    }

    private void Update_MOVING() {
       
    }

    private void Update_SPAWNING() {
        
    }
}
