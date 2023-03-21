using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun&Bullet", menuName = "Gun&Bullet")]
public class Gun_Bullet : ScriptableObject
{
    public Sprite sprite;
    public List<Material> materials;
    public float bulletSpeed;
    public float fireRate;
    //public Transform bulletSpawn; (?)


    //FORSE DA AGGIUNGERE VARIABILI DI DIMENSIONE MESH PER LA TRIGGER BOX

}
