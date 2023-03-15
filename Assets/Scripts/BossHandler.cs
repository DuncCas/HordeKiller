using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{

    //PER ORA STATI VANNO QUA MA LI DEVO METTERE IN UNA NUOVA FUNZIONE SRY NOT SRY
  public enum BOSS_STATES {
        IDLE,
        ARRIVING,
        GROUND,
        LEAVING

    }

    public bool IsComing;

    public GameLogic gameManager;
    public PlayerHandling player;

    BOSS_STATES state;
    BOSS_STATES previousState;

    public float timeBeforeNextStomp;
    public float maxTimeBeforeNextStomp;
    public float timeShowShadow; //Prima quanti tot secondi mostri l'ombra del piede

    public float timeOnGround;
    public float maxTimeOnGround;

    public float fallingSpeed;
    public float raisingSpeed;

    public Transform spawnHeight;

    public GameObject foot;

    #region VAR MOVEMENT
    /*public BossMovement movement;
    public float movementSpeed = 1.5f;
    public float damageOnTouch = 40f;
    public float timeBeforeBossMoves;
    public float maxtimeBeforeBossMoves;
    public float movementDuration;
    public float maxMovementDuration;
    //Se vieni schiacciato muori*/
    #endregion

    #region VAR ENEMY SPAWN
    /* public float timeBetweenNewSpawns; //alcuni nemici partono da lui
     public float maxTimeBeforeNewSpawns;
     public GameObject enemyType;*/
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        state = BOSS_STATES.IDLE;
        previousState = BOSS_STATES.IDLE;
        timeBeforeNextStomp=0;
    //timeBetweenNewSpawns = 0;
    //timeBeforeBossMoves=0;
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
                case BOSS_STATES.ARRIVING:
                    Enter_ARRIVING();
                    break;
                case BOSS_STATES.GROUND:
                    Enter_GROUND();
                    break;
                case BOSS_STATES.LEAVING:
                    Enter_LEAVING();
                    break;
                    /*case BOSS_STATES.SPAWNING:
                        Enter_SPAWNING();
                        break;*/
            }
        }
    }

    private void Enter_LEAVING() {
        //1OP QUANDO SE NE VA PIEDE
    }

    private void Enter_GROUND() {
        //1OP QUANDO ARRIVA AD ESSERE NEL TERRENO
        IsComing = false;
        timeOnGround = 0;
    }

    private void Enter_ARRIVING() {
        //1OP QUANDO ARRIVA
    }

    private void Enter_IDLE() {
        //1OP QUANDO IN IDLE
        foot.SetActive(false);
    }

   

    private void Exit_IDLE() {
        //OP USCITA DA MOVING
    }


    #endregion

    #region UPDATES
    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case BOSS_STATES.IDLE:
                UPDATE_IDLE();
                break;
            case BOSS_STATES.ARRIVING:
                UPDATE_ARRIVING();
                break;
            case BOSS_STATES.GROUND:
                UPDATE_GROUND();
                break;
            case BOSS_STATES.LEAVING:
                UPDATE_LEAVING();
                break;
        }
    }

    private void UPDATE_IDLE() {
        if (timeBeforeNextStomp >= maxTimeBeforeNextStomp) {
            ChangeState(BOSS_STATES.ARRIVING);
        } else {
            timeBeforeNextStomp+= Time.deltaTime;
            if (timeBeforeNextStomp > maxTimeBeforeNextStomp - timeShowShadow) {
                //CODICE PER MOSTRARE OMBRA
                IsComing = true;
            }
        }
    }

    private void UPDATE_ARRIVING() {
        if (!foot.GetComponent<LegBehaviour>().IsOnGround()) {
            foot.transform.position = Vector3.down * fallingSpeed;
        } else {
            ChangeState(BOSS_STATES.GROUND);
        }
    }

    private void UPDATE_GROUND() {
        if (timeOnGround >= maxTimeOnGround) {
            ChangeState(BOSS_STATES.LEAVING);
        } else {
            timeOnGround += Time.deltaTime;
        }
    }

    private void UPDATE_LEAVING() {
        if (foot.transform.position == spawnHeight.position) {
            ChangeState(BOSS_STATES.IDLE);
        } else {
            foot.transform.position += Vector3.up * raisingSpeed;
        }
    }

    private void Update_IDLE() {
        // RIMANE FERMO, MAGARI METTERE DEI EFFETTI TIPO CIOTTOLI CHE CADONO DALL'ALTO O QUALCOSA DEL GENERE
        timeBeforeNextStomp += Time.deltaTime;
        // QUANDO FINISCE TIMER STOMP => STOMP
        if (timeBeforeNextStomp >= maxTimeBeforeNextStomp) {
            ChangeState(BOSS_STATES.ARRIVING);
            return;
        }
    }

 









    /*timeBetweenNewSpawns += Time.deltaTime;
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


}*/
    #endregion
}
/*private void Update_MOVING() {
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
}*/
