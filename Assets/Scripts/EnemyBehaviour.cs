using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float cooldown = 3f; //seconds
    private float lastAttackedAt = -9999f;
    public float hp;
    public float damage;
    public PlayerHandling player;
    //public EnemySpawner spawner; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamaged(float dmg) {
                hp -= dmg;
                if (hp <= 0) {
                    hp = 0;
            gameObject.SetActive(false);
            //mettere nello script dello spawner la lista dei nascosti. Servirà per fare un unica eliminazione una volta che si riempe completamente
                }
            }
        
    

    public void OnTriggerStay(Collider Coll) { 
        if(Coll.gameObject.tag == "Player") {
            player.ChangeLife(damage, false);
            if (Time.time > lastAttackedAt + cooldown) {
                //do the attack
                lastAttackedAt = Time.time;
            }
        }
    }






}
