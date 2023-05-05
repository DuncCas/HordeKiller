using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    public Enemy en;
    public float hp;
    protected int prefId;
    public float Maxcooldown = 3f; //seconds
    public float cooldown;
    public GameObject player;
    public GameObject exp;

    public float maxValueBeforeCheck = 5f;
    float valueBeforeCheck;
    public float distanceToTriggerRespawn=100f;
    EnemySpawner Spawner;
    //public EnemySpawner spawner; 


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        Spawner = player.GetComponent<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = en.hp;
        cooldown = Maxcooldown;
    }

    // Update is called once per frame
    void Update() {
        if (cooldown < Maxcooldown) {
            cooldown += Time.deltaTime;
        }
        if (valueBeforeCheck >= maxValueBeforeCheck) {
            checkDistance();
            valueBeforeCheck = 0;
        }
        valueBeforeCheck += Time.deltaTime;
    }

    private void checkDistance() {
        if (Vector3.Distance(player.transform.position, transform.position) >= distanceToTriggerRespawn) {
            //Se giocatore troppo distante da nemico respawnalo vicino
            //Debug.Log("Distance" + Vector3.Distance(player.transform.position, transform.position));
            transform.position = Spawner.newLocation(prefId);
            hp = en.hp;
        }
        }

    public void ChangeHealth(float dmg, bool gained) {
        hp -= dmg;
                if (hp <= 0) {
                Death();
                }
            }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Projectyle") {
            ChangeHealth(player.GetComponent<PlayerHandling>().damage, false);
        }
    }

    public void OnTriggerStay(Collider Coll) {
        if (Coll.gameObject.tag == "Player" && cooldown >= Maxcooldown) {
                Attack(Coll.gameObject);
                cooldown = 0;
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
            target.GetComponent<PlayerHandling>().ChangeHealth(en.damage, false);
    }
}
