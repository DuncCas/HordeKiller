using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float cooldown = 3f; //seconds
    private float lastAttackedAt = -9999f;
    private float hp;
    public float damage;
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

    public void GetDamaged(float dmg) {
                hp -= dmg;
                if (hp <= 0) {
                OnDeath();
                }
            }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Projectyle") {
            GetDamaged(player.GetComponent<PlayerHandling>().damage);
        }
    }



    public void OnTriggerStay(Collider Coll) {     
        if(Coll.gameObject.tag == "Player") {
            player.GetComponent<PlayerHandling>().ChangeLife(damage, false);
            if (Time.time > lastAttackedAt + cooldown) {
                //do the attack
                lastAttackedAt = Time.time;
            }
        }
    }

    public void OnDeath() {
        hp = 0;
        //Drop exp al 50%
        if (Random.Range(0, 3) < 2) {
            Instantiate(exp, transform.position, transform.rotation);
        }
        //Ricolloco oggetto
        gameObject.SetActive(false);
        EnemySpawner Spawner = player.GetComponent<EnemySpawner>();
        Spawner.Spawned--;
        Vector3 newPosition = Spawner.GetNewPosition();
        transform.position = newPosition;
        hp = Spawner.hp_Enemy;
        gameObject.SetActive(true);
    }





}
