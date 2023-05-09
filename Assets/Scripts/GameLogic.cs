using System.Collections.Generic;
using HordeKiller;
using UnityEngine;

public class GameLogic : MonoBehaviour, IRandomNumberGenerator {

    public static GameLogic instance;
   
    public enum GameState {
        START,
        PHASE1,
        PHASE2,
        VICTORY,
        DEATH,
        END
    }    
    GameState _state = GameState.START;
    GameState _previousState;

    [Header("Player Settings")]
    [Tooltip ("The max number of armor the player must collect to progress." )]
    [Range (1,99)]
    public int maxArmorToCollect;
    [Tooltip ("The player prefab necessary to handle events")]
    public GameObject player;
    [Tooltip ("PHASE1 camera position")]
    public Transform firstCamera;
    [Tooltip("PHASE2 camera position")]
    public Transform playerScndCamera;


    [Header("Civilians Settings")]
    [Tooltip("The civilian prefab")]
    public GameObject civilian;
    [Tooltip("Max distance the Civilian must spawn")]
    [Range(51, 100)]
    public float rangeToSpawnCiv;
    [Tooltip("Minimal distance the civilian must spawn")]
    [Range(10, 50)]
    public float minRangeToSpawnCiv;

    [Header("Boss Settings")]
    //public float bossHeight;
    [Tooltip("The boss scriptable object to reference for the scene")]
    public Boss boss;
    [Tooltip("The boss prefab")]
    public GameObject bossPref;

    //public Camera camera;

    [Header("Exp orbs Settings")]
    [Tooltip("Experience prefab")]
    public GameObject Exp;
    [Tooltip("Size of Experience pool")]
    [Range(8, 99)]
    public int EXPamountToPool;
    List<GameObject> _PooledExp;

    #region INIT

    private void Awake() {
        if (instance) {
            Destroy(this);
        } else {
            instance = this;
        }

    }
    void Start() {
        Instantiate(civilian, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(GenerateRandomValue(minRangeToSpawnCiv,rangeToSpawnCiv)+transform.position.x, 0, GenerateRandomValue(minRangeToSpawnCiv, rangeToSpawnCiv)+transform.position.z), transform.rotation);
        //Instantiate(bossPref, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * bossHeight, transform.rotation);
        bossPref.GetComponent<BossHandler>().data = boss;
        _PooledExp = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < EXPamountToPool; i++) {
            tmp = Instantiate(Exp);
            tmp.SetActive(false);
            _PooledExp.Add(tmp);
        }
        Enter_START();
    }

    #endregion

    #region STATES
    public void ChangeState(GameState newState) {
        if (_state == newState) {
            return;
        }

        _previousState = _state;
            _state = newState;
                Debug.Log("Now playing " + _state + " state. Previous state is " + _previousState);
            switch (_state) {
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

    private void Enter_START() {
        //Inizio partita
        ChangeState(GameState.PHASE1);
    }

    private void Enter_PHASE1() {
        //Inizio fase 1
    }

    private void Enter_PHASE2() {
        //Inizio fase 2
        player.transform.position = new Vector3(0, 400f, 0);
        GetComponent<Camera>().GetComponent<FollowCamera>().followTarget= playerScndCamera;
        GetComponent<Camera>().transform.rotation= Quaternion.Euler(new Vector3(10,0,-90)); //Passo da camera ortografica a camera prospettica;
        GetComponent<Camera>().orthographic = false;
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

    public GameState getGameState() {
        return _state;
    }

    #endregion

    #region EXPERIENCE
    public void SpawnExp(Vector3 position) {
        GameObject tmp = GetPooledObject();
        if (tmp != null) {
            tmp.transform.position = position;
            tmp.SetActive(true);
        } else {
            // se sono tutti attivi gli riporto il pi� lontano
            tmp = findFarest(position);
            tmp.transform.position = position;
        }
    }

    //Trovami il globo pi� lontano e portalo qui
    private GameObject findFarest(Vector3 position) {
        float distance= 0f;
        GameObject tmp = _PooledExp[0];
        foreach(GameObject orb in _PooledExp) {

            if (Vector3.Distance(position, orb.transform.position) > distance) {
                distance = Vector3.Distance(tmp.transform.position, orb.transform.position);
                tmp = orb;
            }
        }

        return tmp;
    }

    private GameObject GetPooledObject() {
        for (int i = 0; i < EXPamountToPool; i++) {
            if (!_PooledExp[i].activeInHierarchy) {
                return _PooledExp[i];
            }
        }
        return null;
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

    public bool checkTotArmor(int armor) {
        if (maxArmorToCollect == armor) {
            ChangeState(GameState.PHASE2);
            return true;
        }
        return false;
    }

    #endregion

    private void GameEnd() {
        //mostrare schermata di uscita o whatev
    }

    public void Death() {
        ChangeState(GameState.DEATH);
    }

}
