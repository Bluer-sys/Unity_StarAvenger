using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePlayerWeapon : PlayerWeapon
{
    private int holeCount = 3;
    private float startSpread = 15f;
    private float spreadDelay = 15f;
    private float spread;

    private void Awake()
    {
        base.Awake();

        ApplyOptions();
    }

    protected override void Shoot()
    {
        for (int i = 0; i < holeCount; i++)
        {
            spread = startSpread - spreadDelay * i;

            if (_bulletContainer.TryGetObjectFromPool(out GameObject bullet))
            {
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, spread);
                _shootParticle.Play();
                _shootSound.Play();
            }
        } 

        StartCoroutineDelayForShoot();
    }

    protected override void ApplyOptions()
    {
        switch (GameSceneController.PlayerData.tripleWeaponTier)
        {
            case 0:
                isBought = false;
                break;
            case 1:
                isBought = true;
                holeCount = 3;
                startSpread = 15f;
                spreadDelay = 15f;
                break;
            case 2:
                isBought = true;
                holeCount = 5;
                startSpread = 30f;
                spreadDelay = 15f;
                _baseShootInterval = 0.25f;
                _shootInterval = 0.25f;
                break;
            case 3:
                isBought = true;
                holeCount = 7;
                startSpread = 45f;
                spreadDelay = 15f;
                _baseShootInterval = 0.20f;
                _shootInterval = 0.20f;
                break;
            case 4:
                isBought = true;
                holeCount = 13;
                startSpread = 90f;
                spreadDelay = 15f;
                _baseShootInterval = 0.15f;
                _shootInterval = 0.15f;
                break;
            default:
                break;
        }
    }
}
