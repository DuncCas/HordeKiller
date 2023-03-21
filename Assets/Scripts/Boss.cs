using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Enemy/Boss")]
public class Boss : ScriptableObject
{
    public Mesh mesh;
    public List<Material> materials;
    public float dmg;
    public float maxTimeBeforeStomp;
    public float fallingSpeed;
    public float maxTimeGround;
    public float risingSpeed;
    //FORSE DA AGGIUNGERE VARIABILI DI DIMENSIONE MESH PER LA TRIGGER BOX
}
