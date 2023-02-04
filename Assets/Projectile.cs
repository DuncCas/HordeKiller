using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    enum states {
        SHOT,
        EXPLODED
    }

    public bool canShoot; // per delay tra un colpo e l'altro
    public float speed;
    public float damage;
    public float life = 3;
    private states status;
    public EnemyBehaviour enemy;
    // Start is called before the first frame update
   

    void Start()
    {
        status = states.SHOT;
    }

    // Update is called once per frame


    private void Awake() {
        Destroy(gameObject, life);
    }




    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            enemy.GetDamaged(damage);
        }
    }
}



