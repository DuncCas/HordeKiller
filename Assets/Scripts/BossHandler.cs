using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{

    //PER ORA STATI VANNO QUA MA LI DEVO METTERE IN UNA NUOVA FUNZIONE SRY NOT SRY
  public enum BOSS_STATES {
        IDLE,
        MOVING,
        SPAWNING
        //ENDFIRSTPHASE
        //ENDGAME

    }
    public GameLogic gameManager;
    public PlayerHandling player;

    BOSS_STATES state;
    BOSS_STATES previousState;

    #region VAR MOVEMENT
    public BossMovement movement;
    public float movementSpeed = 1.5f;
    public float damageOnTouch = 40f;
    public float timeBeforeBossMoves;
    public float maxtimeBeforeBossMoves;
    public float movementDuration;
    public float maxMovementDuration;
    //Se vieni schiacciato muori
    #endregion

    #region VAR ENEMY SPAWN
    public float timeBetweenNewSpawns; //alcuni nemici partono da lui
    public float maxTimeBeforeNewSpawns;
    public GameObject enemyType;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        state = BOSS_STATES.IDLE;
        previousState = BOSS_STATES.IDLE;
        timeBetweenNewSpawns = 0;
        timeBeforeBossMoves=0;
    }

    #region STATES

    public void ChangeState(BOSS_STATES newState) {
        if (state != newState) {
            previousState = state;
            state = newState;
            switch (state) {
                case BOSS_STATES.IDLE:
                    Enter_IDLE();
                    break;
                case BOSS_STATES.MOVING:
                    Enter_MOVING();
                    break;
                case BOSS_STATES.SPAWNING:
                    Enter_SPAWNING();
                    break;
            }
        }
    }

    private void Enter_IDLE() {
        //OP PASSAGGIO IN IDLE
    }

    private void Enter_MOVING() {
        //OP PASSAGGIO A MOVING
    }

    private void Enter_SPAWNING() {
        //OP PASSAGGIO A SPAWNING
    }

    private void Exit_MOVING() {
        //OP USCITA DA MOVING
    }

    private void Exit_SPAWNING() {
        //OP USCITA DA SPAWNING
    }

    #endregion

    #region UPDATES
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




    private void Update_IDLE() {
        // RIMANE FERMO, MAGARI METTERE DEI EFFETTI TIPO CIOTTOLI CHE CADONO DALL'ALTO O QUALCOSA DEL GENERE


        timeBeforeBossMoves += Time.deltaTime;
        // QUANDO FINISCE TIMER MOVIMENTO => MOVING
        if (timeBeforeBossMoves >= maxtimeBeforeBossMoves) {
            ChangeState(BOSS_STATES.MOVING);
            return;
        }
        timeBetweenNewSpawns += Time.deltaTime;
        // QUANDO FINISCE TIMER SPAWN => SPAWNING
        if (timeBetweenNewSpawns >= maxTimeBeforeNewSpawns) { 
            ChangeState(BOSS_STATES.SPAWNING);
            return;
        }
        // SE GIOCATORE OTTIENE TUTTI PEZZI ARMATURA => ENDFIRSTPHASE
        
        // if (gameManager.status== ENDFIRSTPHASE){
        //  ChangeState(BOSS_STATES.ENDFIRSTPHASE)
        //  }
        
        // SE MUORE GIOCATORE => ENDGAME

        
    }

    private void Update_MOVING() {
        //MUOVE LE GAMBE DEL BOSS,
        //movement.MoveTo(Vector3 Direction);
        // SE FINISCE TEMPO DI MOVIMENTO => SE IL TIMER DELLO SPAWN HA FINITO => SPAWN /ELSE/ IDLE
        --movementDuration;
        if ((movementDuration <= 0)&&(movement.AllLegsOnGround())) {
            if (timeBetweenNewSpawns >= maxTimeBeforeNewSpawns) {
                ChangeState(BOSS_STATES.SPAWNING);
            } else {
                ChangeState(BOSS_STATES.IDLE);
            }
        }   
    }

    private void Update_SPAWNING() {
        //OP DI SPAWN
        //Spawn();
        ChangeState(BOSS_STATES.IDLE);
    }
    #endregion
}
