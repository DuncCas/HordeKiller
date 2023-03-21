using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public string mechName;
    public Mesh mesh;
    public List<Material> materials;
    public float Maxhp;
    public float damage;
    public Projectile bullet;


    //FORSE DA AGGIUNGERE VARIABILI DI DIMENSIONE MESH PER LA TRIGGER BOX
}
