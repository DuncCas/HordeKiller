using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{

    void Attack(GameObject target);

    bool Cooldown(float lastAttTime, float cooldwnDuration);

}
