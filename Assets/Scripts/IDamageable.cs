using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {


    void Squashed();


    void ChangeHealth(float tot, bool gained);


    void Death();

    void OnHit();


}
