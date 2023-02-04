using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandling : MonoBehaviour
{
    public GameObject player;

    [Tooltip("Change player starting health")]
    public float Maxhp = 100f;
    public float hp;
    public int lvl = 1;
    public int exp;
    public int MaxExp = 1;
    public Projectile bullet;
    public Transform bulletSpawn;
    public float bulletSpeed =10f;
    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp * lvl;
        exp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.LeftControl)) {
            var proj = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            proj.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
                
        }*/
    }

    public void ChangeLife(float tot, bool gained) {
        if (gained) {
            hp += tot;
            if (hp> Maxhp) {
                hp = Maxhp;
            }
        } else {
            hp -= tot;
            if (hp <= 0) {
                hp = 0;
            }
        }
    }

    public void IncreaseExp() {
        exp++;
        if (exp >= MaxExp) {
            lvlUp();
        }
    }


    public void lvlUp() {
        if (exp == MaxExp) {
            lvl++;
            exp = 0;
            MaxExp = MaxExp * lvl;
            Maxhp = Maxhp * lvl;
            ChangeLife((Maxhp * 0.3f), true);
        }
    }

   


}
