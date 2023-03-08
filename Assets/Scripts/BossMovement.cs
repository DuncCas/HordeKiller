using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BossMovement : MonoBehaviour {
    
    
    //Lista delle gambe in uso
    public List<LegBehaviour> LegList;
    //Velocità di ascesa e discesa
    public float raisingSpeed;
    

    //Serve quando la gamba si alza
    public float actualY;
    public float maxY;



    //Dati generici per comprendere quando passare alla gamba sucessiva
    //public bool[] IsOnGround;
    public int LastLeg;
    public int currentLeg;
    public float offset;


    //Controlla se tutte le gambe sono a terra
    public bool AllLegsOnGround() {
        foreach (LegBehaviour leg in LegList) {
            if (!leg.IsOnGround) {
                return false;
            }
        }
        return true;
    }


    //Op per alzare la gamba. Aggiungerò anche quella per abassare
    public void MovementPrepUp() {
        actualY = raisingSpeed * Time.deltaTime;
        if (actualY >= maxY) {
            actualY = maxY;
        }
            transform.position += Vector3.up * actualY;
        
       
    }

    //Op per spostamento gamba nella nuova direzione, da cambiare perchè fa schifo
    public void MovementDirectional() {
        if (LegList[currentLeg].movingUp) {
            MovementPrepUp();
        } else {
            //transform.position +=
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //init
        foreach (LegBehaviour leg in LegList) {
            leg.IsOnGround = true;
            
        }
        currentLeg = 1;
        //LastLeg = -1;

    }



    // Update is called once per frame
    void Update()
    {
       
    }
}
