using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Maxhp = 100f;
    public float hp;
    public float exp;
    public float lvl = 1f;
    //private List<Weapons> WInventory;      -----Lista Armi in possesso----
    //private List<Abilities> AInventory;    -----Lista Abilità in possesso-----

    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp*lvl;
        exp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeLife(float tot, bool gained) {  //Cambia valore vita, se gained è true incremento hp altrimenti decremento
        if (gained) {
            if (hp + tot < Maxhp) {
                hp += tot;
            } else {
                hp = Maxhp;
            }
        } else {
            if (hp - tot > 0) {
                hp -= tot;
            } else {
                hp = 0;
                //Death();
            }
        }

    }


    public void OnLvlUp() {
        lvl++;
        hp += ((float)lvl * Maxhp) * 0.3f;
        Maxhp = Maxhp * lvl;
    }



}
