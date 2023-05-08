using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpChoice : MonoBehaviour
{
    public void BulletSpeedUp()
    {
        PlayerHandling.instance.bulletSpeed += 1;
    }

    public void FireRateUp()
    {
        PlayerHandling.instance.fireRate += 1;
    }

    public void DamegeUp()
    {
        PlayerHandling.instance.damage += 1;
    }
}