using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpChoice : MonoBehaviour
{
        PlayerHandling player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandling>();
    }
    public void BulletSpeedUp()
    {
        player.bulletSpeed += 1;
    }

    public void FireRateUp()
    {
        player.delayBetweenShots -= 0.1f;
    }

    public void DamageUp()
    {
        player.damage += 1;
    }
}