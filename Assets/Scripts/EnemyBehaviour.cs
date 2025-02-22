using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable, IAttack
{
    [SerializeField]
    public Enemy en;
    int prefId = 0;
    public float cooldown = 3f; //seconds
    private float lastAttackedAt = -9999f; // val di init?
    public GameObject player;
    public GameObject exp;
    //public EnemySpawner spawner; 


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(float dmg, bool gained) {
        en.hp -= dmg;
                if (en.hp <= 0) {
                Death();
                }
            }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Projectyle") {
            ChangeHealth(player.GetComponent<PlayerHandling>().damage, false);
        }
    }



    public bool Cooldown(float lastAttTime, float cooldwnDuration) {
        if (Time.time > lastAttTime + cooldwnDuration) {
            return true;
        } else {
            return false;
        }
    }

    //DA RIFARE IL COOLDOWN NON FUNZIA COME VORREI

    public void OnTriggerStay(Collider Coll) {     
        if(Coll.gameObject.tag == "Player") {
            Attack(Coll.gameObject);     
        }
    }

   virtual public void Death() {
        en.hp = 0;
        //Drop exp al 50%
        if (Random.Range(0, 3) < 2) {
            Instantiate(exp, transform.position, transform.rotation);
        }
        //Ricolloco oggetto
        gameObject.SetActive(false);
        EnemySpawner Spawner = player.GetComponent<EnemySpawner>();
        Spawner.SpawnedSmall--;
        Vector3 newPosition = Spawner.newLocation();
        transform.position = newPosition;
        en.hp = Spawner.hp_Enemy;
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
        if (Cooldown(lastAttackedAt, cooldown)) {
            target.GetComponent<PlayerHandling>().ChangeHealth(en.damage, false);
            lastAttackedAt = Time.time;
        }
    }
}
