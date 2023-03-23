using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    public enum GameState {
        START,
        PHASE1,
        PHASE2,
        VICTORY,
        DEATH,
        END
    }

    public GameState state;
    public GameState previousState;
    public GameObject civilian;
    public float bossHeight;
    public GameObject boss;
    public static GameLogic instance;


    private void Awake() {
        if (instance) {
            Destroy(this);
        } else {
            instance = this;
        }
        Instantiate(civilian, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)), transform.rotation);
        Instantiate(boss, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * bossHeight, transform.rotation);

    }
    void Start() {


    }


    public void ChangeState(GameState newState) {
        if (state != newState) {
            previousState = state;
            state = newState;
            switch (state) {
                case GameState.START:
                    Enter_START();
                    break;
                case GameState.PHASE1:
                    Enter_PHASE1();
                    break;
                case GameState.PHASE2:
                    Enter_PHASE2();
                    break;
                case GameState.VICTORY:
                    Enter_VICTORY();
                    break;
                case GameState.DEATH:
                    Enter_DEATH();
                    break;
                case GameState.END:
                    Enter_END();
                    break;
            }
        }
    }

    private void GameEnd() {

    }

    private void Death() {

    }





    private void Enter_START() {
        //Inizio partita
    }

    private void Enter_PHASE1() {
        //Inizio fase 1
    }

    private void Enter_PHASE2() {
        //Inizio fase 2
    }

    private void Enter_VICTORY() {
        //Vittoria
    }

    private void Enter_DEATH() {
        //Morte
    }

    private void Enter_END() {
        GameEnd();
    }


    // Update is called once per frame
    /* void Update() {
        switch (state) {
            case GameState.START:
                UPDATE_START();
                break;
            case GameState.PHASE1:
                UPDATE_PHASE1();
                break;
            case GameState.PHASE2:
                UPDATE_PHASE2();
                break;
            case GameState.VICTORY:
                UPDATE_VICTORY();
                break;
            case GameState.DEATH:
                UPDATE_DEATH();
                break;
            case GameState.END:
                Enter_END();
                break;
        }
    }

    private void UPDATE_START() {
        //
    }

    private void UPDATE_PHASE1() {
        //
    }

    private void UPDATE_PHASE2() {
        //
    }

    private void UPDATE_VICTORY() {
        //
    }

    private void UPDATE_DEATH() {
       //
    }*/

    
}
