using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandling : MonoBehaviour
{
    [Tooltip("Change player starting health")]
    private GameObject target;  //Per i test switcharlo a public
    public float Maxhp = 100f;
    public float damage=3f;
    private float hp;
    public int lvl = 1;
    private int exp;
    public int MaxExp = 1;
    public Projectile bullet;
    public Transform bulletSpawn;
    public float bulletSpeed =10f;
    public float fireRate = 3f;
    private float nextFire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp * lvl;
        exp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire) { 
            //canShoot = false;
            //StartCoroutine("AllowToShoot");
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy"); //Genera memoryleak da fixare
            if (allTargets != null) {   //manca settare l'eccezzione nel caso non ci siano nemici altrimenti mi sbrocca unity
                target = allTargets[0];
                foreach (GameObject tmptarget in allTargets) {
                    if (Vector3.Distance(transform.position, tmptarget.transform.position) < Vector3.Distance(transform.position, target.transform.position)) {
                        target = tmptarget;
                    }
                }
                    Fire();
                nextFire = Time.time + fireRate; 
                }
        }
    }

    void Fire() {
        Debug.Log("Shooting " + target.name);
        Vector3 direction = target.transform.position - transform.position;
        //link to spawned arrow, you dont need it, if the arrow has own moving script
        Projectile tmpbullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        tmpbullet.transform.right = direction;
        tmpbullet.GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
    }


    /*IEnumerator AllowToShoot() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }*/

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
