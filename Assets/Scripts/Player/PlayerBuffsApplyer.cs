using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player), typeof(PlayerWeapon))]
public class PlayerBuffsApplyer : MonoBehaviour
{
    [SerializeField] private BuffsViewer _buffsViewer;

    private Player _player;
    private PlayerWeapon[] _playerWeapons;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerWeapons = GetComponent<PlayerShooting>().Weapons;
    }

    #region Fast Shooting Buff
    private Coroutine fastShootingCoroutine;
    private bool isFastShootingCoroutineStart = false;

    public void ApplyFastShootingBuff(float duration, float newShootInterval, Sprite image)
    {
        if (isFastShootingCoroutineStart)
        {
            StopCoroutine(fastShootingCoroutine);
            isFastShootingCoroutineStart = false;
            
            if (!_buffsViewer.TryResetView(image))
                _buffsViewer.View(image, duration);
        }
        else _buffsViewer.View(image, duration);

        fastShootingCoroutine = StartCoroutine(FastShootingCoroutine(duration, newShootInterval));
    }

    private IEnumerator FastShootingCoroutine(float duration, float newShootInterval)
    {
        isFastShootingCoroutineStart = true;
        foreach (var weapon in _playerWeapons)
        {
            weapon.SetShootInterval(weapon.BaseShootInterval * newShootInterval);
        }

        yield return new WaitForSeconds(duration);

        foreach (var weapon in _playerWeapons)
        {
            weapon.SetShootInterval(weapon.BaseShootInterval);
        }
        isFastShootingCoroutineStart = false;
    }
    #endregion

    #region Invincible Buff
    private Coroutine invincibleCoroutine;
    private bool isinvincibleCoroutineStart = false;

    public void ApplyInvincibleBuff(float duration, Sprite image)
    {
        if (isinvincibleCoroutineStart)
        {
            StopCoroutine(invincibleCoroutine);
            isinvincibleCoroutineStart = false;

            if (!_buffsViewer.TryResetView(image))
                _buffsViewer.View(image, duration);
        }
        else _buffsViewer.View(image, duration);

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(duration));
    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        isinvincibleCoroutineStart = true;
        _player.SwitchInvincible(true);

        yield return new WaitForSeconds(duration);

        _player.SwitchInvincible(false);
        isinvincibleCoroutineStart = false;
    }
    #endregion
}
