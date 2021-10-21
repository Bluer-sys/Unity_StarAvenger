using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownAndShoot : StopAndShootState
{
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _minMoveSpeed;

    private float randomMoveSpeed;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (_minMoveSpeed > _maxMoveSpeed)
            _maxMoveSpeed = _minMoveSpeed + 1;
    }

    public override void Enter()
    {
        base.Enter();

        elapsedTimeForShootDelay = 0;

        randomMoveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
        randomShootDelay = Random.Range(_minShootDelay, _maxShootDelay);
    }

    public override void LogicUpdate()
    {
        elapsedTimeForShootDelay += Time.deltaTime;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        transform.Translate(Vector3.up * randomMoveSpeed * Time.fixedDeltaTime);
    }

    protected override void Shoot()
    {
        if (elapsedTimeForShootDelay >= randomShootDelay)
        {
            if (bulletPool.TryGetObjectFromPool(out GameObject bullet))
            {
                bullet.SetActive(true);
                bullet.transform.position = _shootPoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 180);

                _shootEffect.Play();
                _shootSound.Play();
            }

            elapsedTimeForShootDelay = 0;
        }
    }
}
