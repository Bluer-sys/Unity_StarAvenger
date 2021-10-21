using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPlayerWeapon : PlayerWeapon
{
    [SerializeField] private int _lazerCount;
    [SerializeField] private int _damage;

    private LineRenderer[] lines;
    private int currentLazerCount;
    private bool isActive;
    private PlayerShooting playerShooting;
    private LazerRadius lazerRadius;
    private List<Enemy> enemiesInRadius = new List<Enemy>();

    public int LazerCount => _lazerCount;
    public int CurrentLazerCount { get => currentLazerCount; set => currentLazerCount = value; }
    public bool IsActive => isActive;
    public List<Enemy> EnemiesInRadius => enemiesInRadius;
    public LineRenderer[] Lines => lines;

    protected void Awake()
    {
        base.Awake();

        ApplyOptions();

        playerShooting = FindObjectOfType<PlayerShooting>();

        lines = GetComponentsInChildren<LineRenderer>();
        lazerRadius = GetComponentInChildren<LazerRadius>();

        foreach (var line in lines)
        {
            line.positionCount = 2;
            line.startWidth = 0.3f;
        }

        isActive = false;
        gameObject.SetActive(false);

        playerShooting.WeaponChanged += OnWeaponChanged;
    }

    private void OnDestroy()
    {
        playerShooting.WeaponChanged -= OnWeaponChanged;
    }

    private void OnEnable()
    {
        isShootDelayCoroutineStart = false;

        DisableLines();
    }

    private void FixedUpdate()
    {
        DisableLines();

        if (playerShooting.PointerHeld)
        {
            RenderLines();

            if (!_shootParticle.gameObject.activeSelf && EnemiesInRadius.Count > 0)
            {
                _shootParticle.gameObject.SetActive(true);
                _shootSound.gameObject.SetActive(true);
            }
            else if (_shootParticle.gameObject.activeSelf && EnemiesInRadius.Count <= 0)
            {
                _shootParticle.gameObject.SetActive(false);
                _shootSound.gameObject.SetActive(false);
            }
        }
        else
        {
            lazerRadius.gameObject.SetActive(false);

            if (_shootParticle.gameObject.activeSelf)
            {
                _shootParticle.gameObject.SetActive(false);
                _shootSound.gameObject.SetActive(false);
            }
        }
    }

    protected override void Shoot()
    {
        if (enemiesInRadius.Count != 0)
        {
            for (int i = 0; i < enemiesInRadius.Count; i++)
            {
                enemiesInRadius[i].TakeDamage(_damage);
            }

            StartCoroutineDelayForShoot();
        }
    }

    private void OnWeaponChanged()
    {
        if (playerShooting.CurrentWeapon == this)
        {
            isActive = true;
            gameObject.SetActive(true);
        }
        else
        { 
            isActive = false;
            gameObject.SetActive(false);
        }
    }

    private void DisableLines()
    {
        foreach (var line in lines)
        {
            line.enabled = false;
        }
    }

    private void RenderLines()
    {
        lazerRadius.gameObject.SetActive(true);

        for (int i = 0; i < enemiesInRadius.Count; i++)
        {
            lines[i].enabled = true;
            lines[i].SetPosition(0, new Vector3(enemiesInRadius[i].transform.position.x, enemiesInRadius[i].transform.position.y));
            lines[i].SetPosition(1, new Vector3(_shootPoint.position.x, _shootPoint.position.y));
        }
    }

    public void StartCoroutineResetCollider(Enemy enemy)
    {
        StartCoroutine(ResetCollider(enemy));
    }

    private IEnumerator ResetCollider(Enemy enemy)
    {
        if (enemiesInRadius.Remove(enemy))
            currentLazerCount--;

        lazerRadius.Collider2D.size = Vector2.zero;
        yield return new WaitForEndOfFrame();
        lazerRadius.Collider2D.size = lazerRadius.StandardColliderSize;
    }

    protected override void ApplyOptions()
    {
        switch (GameSceneController.PlayerData.lazerTier)
        {
            case 0:
                isBought = false;
                break;
            case 1:
                isBought = true;
                _lazerCount = 1;
                _damage = 2;
                break;
            case 2:
                isBought = true;
                _lazerCount = 2;
                _damage = 3;
                break;
            case 3:
                isBought = true;
                _lazerCount = 3;
                _damage = 5;
                break;
            case 4:
                isBought = true;
                _lazerCount = 5;
                _damage = 6;
                break;
            case 5:
                isBought = true;
                _lazerCount = 6;
                _damage = 8;
                break;
            default:
                break;
        }
    }
}
