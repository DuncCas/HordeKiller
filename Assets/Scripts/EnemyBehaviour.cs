using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float cooldown = 3f; //seconds
    private float lastAttackedAt = -9999f;
    public float hp;
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
                    hp = 0;
            if (Random.Range(0, 3) < 2) {
                Instantiate(exp, transform.position, transform.rotation);
            }
            gameObject.SetActive(false);
            player.GetComponent<EnemySpawner>().OnDeathEnemy();
            //mettere nello script dello spawner la lista dei nascosti. Servirà per fare un unica eliminazione una volta che si riempe completamente
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






}
