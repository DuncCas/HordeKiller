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




    public ShadowFollow footShadow; //RECUPERO L'OMBRA
    public float dmg;

    public Vector3 returnPos;

    private GameObject player;

    public BOSS_STATES state;
    public BOSS_STATES previousState;

    public float maxTimeBeforeStomp;
    public float timeBeforeStomp;

    public float fallingSpeed;

    public float maxTimeGround;
    public float TimeGround;

    public float risingSpeed;

    public bool OnGround;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ChangeState(BOSS_STATES.IDLE);
        previousState = BOSS_STATES.IDLE;
        footShadow.activate = false;
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
            }
        }
    }
   private void Enter_IDLE() {
        if (player) {
            timeBeforeStomp = 0;
        } else {
            gameObject.SetActive(false);
        }

    }

  private void Enter_ARRIVING() {
        //1OP QUANDO ARRIVA
        footShadow.activate = true;
        Vector3 newPosFoot = player.transform.position + Vector3.up * transform.position.y;
        transform.position = newPosFoot;
        returnPos = newPosFoot;


    } 
    
    private void Enter_GROUND() {
        //1OP QUANDO ARRIVA AD ESSERE NEL TERRENO
        footShadow.activate = false;
        TimeGround = 0;
    }
    private void Enter_LEAVING() {
        OnGround = false;
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
        timeBeforeStomp += Time.deltaTime;
        if (timeBeforeStomp >= maxTimeBeforeStomp) {
            ChangeState(BOSS_STATES.ARRIVING);
        }
    }

    private void UPDATE_ARRIVING() {
        transform.position += Vector3.down * Time.deltaTime * fallingSpeed;
        if (OnGround) {
            ChangeState(BOSS_STATES.GROUND);
            
        }
    }

    private void UPDATE_GROUND() {
        TimeGround += Time.deltaTime;
        if (TimeGround >= maxTimeGround) {
            ChangeState(BOSS_STATES.LEAVING);
        }
    }

    private void UPDATE_LEAVING() {
        transform.position += Vector3.up * risingSpeed * Time.deltaTime;
        if (transform.position.y >= returnPos.y) {
            ChangeState(BOSS_STATES.IDLE);
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 9) {
            OnGround = true;
        }
        if (collision.gameObject.tag == "Player"){
            if (!OnGround) {
                player.GetComponent<PlayerHandling>().Squashed();
            } else {
                player.GetComponent<PlayerHandling>().ChangeHealth(dmg, false);
                //Creare funzione di cooldown;
            }
        }
        if (collision.gameObject.tag == "Enemy") {
            if (!OnGround) {
                collision.gameObject.GetComponent<EnemyBehaviour>().Squashed();
            } else {
                collision.gameObject.GetComponent<EnemyBehaviour>().ChangeHealth(dmg, false);
            }
        }
        }
    }
