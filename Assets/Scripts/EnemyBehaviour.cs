using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Header("General")]
    [Tooltip("Player prefab NOTE: Must remain none, the player gets recovered on runtime")]
    public PlayerHandling player;
    [Tooltip("Experience prefab")]
    public GameObject exp;
    [Tooltip("Scriptable Object of enemy type to associate with the prefab")]
    public Enemy en;
    public float hp;
    protected int prefId;
    [Tooltip("Cooldown between attacks")]
    public float Maxcooldown = 3f; //seconds
    float _cooldown;
    [Header("Respawn settings")]
    [Tooltip("Time to pass before checking if enemy is far from player")]
    public float maxValueBeforeCheck = 5f;
    [Tooltip("From how much far from player the enemy has to respawn")]
    public float distanceToTriggerRespawn=100f;
    float _valueBeforeCheck;
    EnemySpawner Spawner;


    private void Awake() {
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        player = tmp.GetComponent<PlayerHandling>();
        Spawner = tmp.GetComponent<EnemySpawner>();
    }

    void Start()
    {
        hp = en.hp;
        _cooldown = Maxcooldown;
    }

    void Update() {
        if (_cooldown < Maxcooldown) {
            _cooldown += Time.deltaTime;
        }
        if (_valueBeforeCheck >= maxValueBeforeCheck) {
            checkDistance();
            _valueBeforeCheck = 0;
        }
        _valueBeforeCheck += Time.deltaTime;
    }

    private void checkDistance() {
        if (Vector3.Distance(player.transform.position, transform.position) >= distanceToTriggerRespawn) {
            //Se giocatore troppo distante da nemico respawnalo vicino
            transform.position = Spawner.newLocation(prefId);
            hp = en.hp;
        }
    }
    #region DAMAGE AND ATTACK

    public void ChangeHealth(float dmg, bool gained) {
        hp -= dmg;
                if (hp <= 0) {
                Death();
                }
            }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Projectyle") {
            ChangeHealth(player.damage, false);
        }
    }

    public void OnTriggerStay(Collider Coll) {
        if (Coll.gameObject.tag == "Player" && _cooldown >= Maxcooldown) {
                Attack(Coll.gameObject);
                _cooldown = 0;
        }
    }

    virtual public void Death() {
        hp = 0;
        //Drop exp al 50%
        if (Random.Range(0, 3) < 2) {
            GameLogic.instance.SpawnExp(transform.position);
        }
        //Ricolloco oggetto
        gameObject.SetActive(false);
        //Vector3 newPosition = 
        transform.position=Spawner.newLocation(prefId);
        hp = en.hp;
        //transform.position = newPosition;
        gameObject.SetActive(true);
    }


    public void Squashed() {
        Death();
        //codice per lasciare chiazza di nemico morto
    }

 


    public void OnHit() {
        //Effetti di quando muore enemy
    }

    public void Attack(GameObject target) {
            player.ChangeHealth(en.damage, false);
    }
    #endregion
}
