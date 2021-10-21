using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerWeapon : PlayerWeapon
{
    [SerializeField] private Transform[] _additionalShootPoints;

    private bool useAdditionalShootPoints;
    private bool useMainShootPoint;

    protected void Start()
    {
        isBought = true;
        ApplyOptions();
    }

    protected override void Shoot()
    {
        if(useMainShootPoint)
        {
            if (_bulletContainer.TryGetObjectFromPool(out GameObject bullet))
            {
                bullet.SetActive(true);
                bullet.transform.position = _shootPoint.position;
            }
        }

        if (useAdditionalShootPoints)
        {
            for (int i = 0; i < _additionalShootPoints.Length; i++)
            {
                if (_bulletContainer.TryGetObjectFromPool(out GameObject bullet))
                {
                    bullet.SetActive(true);
                    bullet.transform.position = _additionalShootPoints[i].position;
                }
            }
        }
        _shootParticle.Play();
        _shootSound.Play();

        StartCoroutineDelayForShoot();
    }

    protected override void ApplyOptions()
    {
        switch (GameSceneController.PlayerData.simpleWeaponTier)
        {
            case 1:
                useMainShootPoint = true;
                useAdditionalShootPoints = false;
                break;
            case 2:
                useMainShootPoint = false;
                useAdditionalShootPoints = true;
                break;
            case 3:
                useMainShootPoint = true;
                useAdditionalShootPoints = true;
                break;
            case 4:
                useMainShootPoint = true;
                useAdditionalShootPoints = true;
                _baseShootInterval = 0.15f;
                _shootInterval = 0.15f;
                break;
            default:
                break;
        }
    }
}
