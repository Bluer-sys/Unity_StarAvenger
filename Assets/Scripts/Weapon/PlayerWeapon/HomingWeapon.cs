using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingWeapon : PlayerWeapon
{
    [SerializeField] private Transform[] _additionalShootPoints;

    private float startBulletRotate;
    private float stepBulletRotate;

    private bool useMainShootPoint;
    private int holeCount;

    private void Start()
    {
        ApplyOptions();
    }

    protected override void Shoot()
    {
        float rotate;

        for (int i = 0; i < _additionalShootPoints.Length; i++)
        {
            rotate = startBulletRotate;

            for (int j = 0; j < holeCount; j++)
            {
                if (_bulletContainer.TryGetObjectFromPool(out GameObject bullet))
                {
                    bullet.SetActive(true);
                    bullet.transform.position = _additionalShootPoints[i].position;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, rotate);
                }

                rotate += stepBulletRotate;
            }
        }

        if (useMainShootPoint)
        {
            rotate = -5f;

            for (int j = 0; j < 2; j++)
            {
                if (_bulletContainer.TryGetObjectFromPool(out GameObject bullet))
                {
                    bullet.SetActive(true);
                    bullet.transform.position = _shootPoint.position;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, rotate);
                }

                rotate += 10f;
            }
        }
        
        _shootParticle.Play();
        _shootSound.Play();

        StartCoroutineDelayForShoot();
    }

    protected override void ApplyOptions()
    {
        switch (GameSceneController.PlayerData.homingWeaponTier)
        {
            case 0:
                isBought = false;
                break;
            case 1:
                isBought = true;
                useMainShootPoint = false;
                holeCount = 2;
                startBulletRotate = -10f;
                stepBulletRotate = 20f;
                break;
            case 2:
                isBought = true;
                useMainShootPoint = true;
                holeCount = 2;
                startBulletRotate = -10f;
                stepBulletRotate = 20f;
                _baseShootInterval = 0.17f;
                _shootInterval = 0.17f;
                break;
            case 3:
                isBought = true;
                useMainShootPoint = true;
                holeCount = 4;
                startBulletRotate = -30f;
                stepBulletRotate = 20f;
                _baseShootInterval = 0.15f;
                _shootInterval = 0.15f;
                break;
            default:
                break;
        }
    }
}
