using System.Collections;
using System.Collections.Generic;
using StarterAssets;
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

    public int maxArmorToCollect;
    public GameObject player;
    public Transform firstCamera;
    public Transform playerScndCamera;
    public GameState state;
    public GameState previousState;
    public GameObject civilian;
    public float bossHeight;
    public Boss boss;
    public GameObject bossPref;
    public static GameLogic instance;
    public Camera camera;
    public int EXPamountToPool;
    List<GameObject> PooledExp;
    public GameObject Exp;

    private void Awake() {
        if (instance) {
            Destroy(this);
        } else {
            instance = this;
        }

    }
    void Start() {
        Instantiate(civilian, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)), transform.rotation);
        Instantiate(bossPref, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * bossHeight, transform.rotation);
        bossPref.GetComponent<BossHandler>().data = boss;
        PooledExp = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < EXPamountToPool; i++) {
            tmp = Instantiate(Exp);
            tmp.SetActive(false);
            PooledExp.Add(tmp);
        }
        state = GameState.START;
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

    public bool checkTotArmor(int armor) {
        if (maxArmorToCollect == armor) {
            ChangeState(GameState.PHASE2);
            return true;
        }
        return false;
    }


    private void GameEnd() {
        //mostrare schermata di uscita o whatev
    }

    public void Death() {
        ChangeState(GameState.DEATH);
    }





    private void Enter_START() {
        //Inizio partita
        //camera.followTarget = firstCamera;
        ChangeState(GameState.PHASE1);
    }

    private void Enter_PHASE1() {
        //Inizio fase 1
    }

    private void Enter_PHASE2() {
        //Inizio fase 2
        player.transform.position = new Vector3(0, 400f, 0);
        camera.GetComponent<FollowCamera>().followTarget= playerScndCamera;
        camera.transform.rotation= Quaternion.Euler(new Vector3(10,0,-90)); //Passo da camera ortografica a camera prospettica;
        camera.orthographic = false;
        player.GetComponent<EnemySpawner>().depopulate();
    }

    private void Enter_VICTORY() {
        //Vittoria
    }

    private void Enter_DEATH() {
        //Morte
        ChangeState(GameState.END);
    }

    private void Enter_END() {
        GameEnd();
    }



    public void SpawnExp(Vector3 position) {
        GameObject tmp = GetPooledObject();
        if (tmp != null) {
            tmp.transform.position = position;
            tmp.SetActive(true);
        } else {
            // se sono tutti attivi gli riporto il più lontano
            tmp = findFarest(position);
            tmp.transform.position = position;
        }
    }

    //Trovami il globo più lontano e portalo qui
    private GameObject findFarest(Vector3 position) {
        float distance= 0f;
        GameObject tmp = PooledExp[0];
        foreach(GameObject orb in PooledExp) {

            if (Vector3.Distance(position, orb.transform.position) > distance) {
                distance = Vector3.Distance(tmp.transform.position, orb.transform.position);
                tmp = orb;
            }
        }

        return tmp;
    }

    private GameObject GetPooledObject() {
        for (int i = 0; i < EXPamountToPool; i++) {
            if (!PooledExp[i].activeInHierarchy) {
                return PooledExp[i];
            }
        }
        return null;
    }



}
