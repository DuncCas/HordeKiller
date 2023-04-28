using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField]
    public Enemy en;
    int prefId = 0;
    public float Maxcooldown = 3f; //seconds
    public float cooldown;
    private float lastAttackedAt = -9999f; // val di init?
    public GameObject player;
    public GameObject exp;
    //public EnemySpawner spawner; 


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = Maxcooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < Maxcooldown) {
            cooldown += Time.deltaTime;
        }
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

    public void OnTriggerStay(Collider Coll) {
        if (Coll.gameObject.tag == "Player" && cooldown >= Maxcooldown) {
                Attack(Coll.gameObject);
                cooldown = 0;
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
        //Vector3 newPosition = 
            Spawner.newLocation(0);
        //transform.position = newPosition;
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
            target.GetComponent<PlayerHandling>().ChangeHealth(en.damage, false);
    }
}
