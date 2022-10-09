using System.Collections;
using DefaultNamespace.UI;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerShooting : ObjectPool
{
    [SerializeField] private PlayerWeapon[] _weapons;
    [SerializeField] private AudioSource _weaponSwitchSound;

    [Inject] private IUiView        _uiView;
    [Inject] private IEnemySpawner  _enemySpawner;

    private bool isLevelFinished = false;

    private PlayerInput _input;
    private bool pointerHeld = false;
    private PlayerWeapon currentWeapon;

    private Coroutine changeWeaponCoroutine;

    public UnityAction WeaponChanged;

    public PlayerWeapon CurrentWeapon => currentWeapon;
    public PlayerWeapon[] Weapons => _weapons;
    public bool PointerHeld => pointerHeld;

    private void Awake()
    {
        _input = new PlayerInput();

        currentWeapon = _weapons[0];
    }

    private void Start()
    {
        WeaponChanged?.Invoke();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Shooting.Shoot.performed += ctx => pointerHeld = true;
        _input.Shooting.Shoot.canceled += ctx => pointerHeld = false;

        _input.Shooting.Weapon1.performed += ctx => TryChangeWeapon(0);
        _input.Shooting.Weapon2.performed += ctx => TryChangeWeapon(1);
        _input.Shooting.Weapon3.performed += ctx => TryChangeWeapon(2);
        _input.Shooting.Weapon4.performed += ctx => TryChangeWeapon(3);

        _input.UI.Menu.performed += ctx => OnMenuSwitch();

        _enemySpawner.AllWavesSpawned += DisableMenu;
    }

    private void OnDisable()
    {
        _input.Disable();

        _enemySpawner.AllWavesSpawned -= DisableMenu;
    }

    private void FixedUpdate()
    {
        if (pointerHeld)
            currentWeapon.TryShoot();
        else return;
    }

    private void TryChangeWeapon(int targetWeapon)
    {
        if (!_weapons[targetWeapon].IsBought)
            return;

        if (currentWeapon != _weapons[targetWeapon])
        {
            if (changeWeaponCoroutine != null)
                StopCoroutine(changeWeaponCoroutine);

            changeWeaponCoroutine = StartCoroutine(ChangeWeapon(targetWeapon));
        }
    }

    private IEnumerator ChangeWeapon(int weaponNumber)
    {
        yield return new WaitForSeconds(0.1f);
        currentWeapon = _weapons[weaponNumber];
        _weaponSwitchSound.Play();

        WeaponChanged?.Invoke();

        changeWeaponCoroutine = null;
    }

    private void DisableMenu()
    {
        _input.UI.Menu.Disable();
    }

    private void OnMenuSwitch()
    {
        _uiView.SwitchMenuActive();

        Time.timeScale = 1 - Time.timeScale;

        Cursor.visible = !Cursor.visible;
    }
}
