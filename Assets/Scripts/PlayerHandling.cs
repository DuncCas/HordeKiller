using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandling : MonoBehaviour
{
    [Header("Damage and Health")]
    [Tooltip("Change player init max health.")]
    public float startingHp;
    [Tooltip("Change player starting damage")]
    public float damage = 3f;
    float _maxHp;
    float _hp;

    [Header("Experience settings")]
    [Tooltip("Class to handle ExpBar of the experience bar")]
    public Experience_bar ExpBar;
    [Tooltip("Change player starting max exp to reach for level up")]
    public float startingMaxExp;
    [Tooltip("Set the increment of maxExp for every level up")]
    public float valueIncreaseExp = 0.3f;
    float _MaxExp = 1;
    float _currentExp=0;
    int _lvl = 1;
    [Header("Shoot settings")]
    [Tooltip("Bullet prefab used by the player")]
    public GameObject bullet;
    [Tooltip("The position over wich the bullet shoots from")]
    public Transform bulletSpawn;
    [Tooltip("Sets the bullet speed")]
    public float bulletSpeed = 10f;
    [Tooltip("Sets bullet rate of fire")]
    public float delayBetweenShots;
    [Tooltip("Sets total number of bullets to the pool")]
    public int amountToPool;
    List<GameObject> _pooledObjects;
    bool _canShoot;
    GameObject _target;  //Per i test switcharlo a public

    [Header("Armor settings")]
    [Tooltip("The armor UI text")]
    public TextMeshProUGUI armorText;
    int _armor = 0;

    #region INIT

    private void Awake() {
        armorText.text = _armor.ToString() + "/" + GameLogic.instance.maxArmorToCollect.ToString();
        _MaxExp = startingMaxExp;
        _maxHp = startingHp;
    }

    // Start is called before the first frame update
    void Start()
    {
        _canShoot = true;
        _pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(bullet);
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);
        }
        _hp = _maxHp * _lvl;
        _currentExp = 0;
        ExpBar.SetMaxExp();

    }

    #endregion



    // Update is called once per frame
    void Update()
    {
        if (_canShoot) {
            //canShoot = false;
            //StartCoroutine("AllowToShoot");
            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy"); //Genera memoryleak da fixare
            //GameObject[] someTargets = new GameObject[5];
            //System.Array.Copy(allTargets, someTargets, 5);
            if (allTargets != null && allTargets.Length > 0) {//manca settare l'eccezzione nel caso non ci siano nemici altrimenti mi sbrocca unity
                _target = allTargets[0];
                foreach (GameObject tmptarget in allTargets) {
                    if (Vector3.Distance(transform.position, tmptarget.transform.position) < Vector3.Distance(transform.position, _target.transform.position)) {
                        _target = tmptarget;
                    }
                }
                    Fire();
                }
        }
    }

    #region SHOOT
    void Fire() {
        Debug.Log("Shooting " + _target.name);
        Vector3 direction = _target.transform.position - transform.position;
        //link to spawned arrow, you dont need it, if the arrow has own moving script
        GameObject bullet = GetPooledObject();
        if (bullet != null) {
            Debug.Log("Creating bullet");
            bullet.SetActive(true);
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.right = direction;
            bullet.GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
            _canShoot = false;
            StartCoroutine("ShootDelay");
        } else {
            Debug.Log("no bullets");
        }
    }

    GameObject GetPooledObject() {
        for (int i = 0; i < amountToPool; i++) {
            if (!_pooledObjects[i].activeInHierarchy) {
                return _pooledObjects[i];
            }
        }
        return null;
    }
    public float GetDamage() {
        return damage;
    }
    IEnumerator ShootDelay() {
        yield return new WaitForSeconds(delayBetweenShots);
        _canShoot = true;
    }
    #endregion

    #region HEALTH
    public void ChangeHealth(float tot, bool gained) {
        if (gained) {
            _hp += tot;
            if (_hp > _maxHp) {
                _hp = _maxHp;
            }
        } else {
            _hp -= tot;
            if (_hp <= 0) {
                _hp = 0;
            }
        }
    }
    public void Squashed() {
        ChangeHealth(_hp, false);
    }

    public float GetHealth() {
        return _hp;
    }
    public void revive() {
        _maxHp = startingHp;
        _hp = _maxHp;
        _MaxExp = startingMaxExp;
        _lvl = 1;
        _currentExp = 0;
    }

    #endregion

    #region EXPERIENCE

    public float GetMaxExp() {
        return _MaxExp;
    }

    public void IncreaseExp() {
        _currentExp += valueIncreaseExp;
        ExpBar.SetExp(_currentExp);
        if (_currentExp >= _MaxExp) {
            lvlUp();
        }

    }


    void lvlUp() {
        if (_currentExp >= _MaxExp) {
            _lvl++;
            _currentExp = 0;
            valueIncreaseExp = valueIncreaseExp / 2;
            //MaxExp = MaxExp * lvl;
            _maxHp = _maxHp * _lvl;
            ExpBar.SetMaxExp();
            ChangeHealth((_maxHp * 0.3f), true);
        }
    }

    #endregion
    public void IncreaseArmor() {
        _armor += 1;
        armorText.text = _armor.ToString() + "/" + GameLogic.instance.maxArmorToCollect.ToString();
        GameLogic.instance.checkTotArmor(_armor);
    }
}
