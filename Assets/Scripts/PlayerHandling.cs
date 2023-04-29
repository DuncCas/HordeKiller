using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandling : MonoBehaviour
{
    [Tooltip("Change player starting health")]
    public Experience_bar ExpBar;
    private GameObject target;  //Per i test switcharlo a public
    public float Maxhp = 100f;
    public float damage = 3f;
    public float hp;
    public int lvl = 1;
    public float exp;
    public float MaxExp = 1;
    public GameObject bullet;
    public List<GameObject> pooledObjects;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    //private float nextFire = 0f;
    public int amountToPool;
    private bool canShoot;

    public float delayBetweenShots;

    public TextMeshProUGUI armorText;
    private int armor = 0;
    public Experience_bar expBar;
    private float valueIncreaseExp = 0.3f;

    

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(bullet);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        hp = Maxhp * lvl;
        exp = 0;
        ExpBar.SetMaxExp();

    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < amountToPool; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public float GetDamage() {
        return damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) {
            //canShoot = false;
            //StartCoroutine("AllowToShoot");
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy"); //Genera memoryleak da fixare
            //GameObject[] someTargets = new GameObject[5];
            //System.Array.Copy(allTargets, someTargets, 5);
            if (allTargets != null && allTargets.Length > 0) {//manca settare l'eccezzione nel caso non ci siano nemici altrimenti mi sbrocca unity
                target = allTargets[0];
                foreach (GameObject tmptarget in allTargets) {
                    if (Vector3.Distance(transform.position, tmptarget.transform.position) < Vector3.Distance(transform.position, target.transform.position)) {
                        target = tmptarget;
                    }
                }
                    Fire();
                }
        }
    }

    void Fire() {
        Debug.Log("Shooting " + target.name);
        Vector3 direction = target.transform.position - transform.position;
        //link to spawned arrow, you dont need it, if the arrow has own moving script
        GameObject bullet = GetPooledObject();
        if (bullet != null) {
            Debug.Log("Creating bullet");
            bullet.SetActive(true);
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.right = direction;
            bullet.GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
            canShoot = false;
            StartCoroutine("ShootDelay");
        } else {
            Debug.Log("no bullets");
        }
    }


    public void ChangeHealth(float tot, bool gained) {
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
        exp += valueIncreaseExp;
        ExpBar.SetExp(exp);
        if (exp >= MaxExp) {
            lvlUp();
        }

    }


    public void lvlUp() {
        if (exp >= MaxExp) {
            lvl++;
            exp = 0;
            valueIncreaseExp = valueIncreaseExp / 2;
            //MaxExp = MaxExp * lvl;
            Maxhp = Maxhp * lvl;
            ExpBar.SetMaxExp();
            ChangeHealth((Maxhp * 0.3f), true);
        }
    }

    public void Squashed() { }
    public void IncreaseArmor() {
        armor += 1;
        armorText.text = armor.ToString();
        GameLogic.instance.checkTotArmor(armor);
    }
    IEnumerator ShootDelay() {
        yield return new WaitForSeconds(delayBetweenShots);
        canShoot = true;
    }

}
