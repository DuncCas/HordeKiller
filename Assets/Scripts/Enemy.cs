using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy/Normal")]
public class Enemy : ScriptableObject
{
    //VARIABILE DEL MODELLO VA QUA
    
    public Mesh mesh;
    public List<Material> materials;
    public string enemyName;
    public float hp;
    public float damage;
    //FORSE DA AGGIUNGERE VARIABILI DI DIMENSIONE MESH PER LA TRIGGER BOX
}
