using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    protected EffectsPool explosionEffect;

    public int Damage => _damage;
    protected float Speed { get => _speed; set => _speed = value; }

    protected abstract void Move();
}
