using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewer : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private Image _imageContainer;
    [SerializeField] private TMP_Text _textContainer;

    private void OnEnable()
    {
        _playerShooting.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _playerShooting.WeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged()
    {
        _imageContainer.sprite = _playerShooting.CurrentWeapon.Icon;
        _textContainer.text = _playerShooting.CurrentWeapon.Name;
    }
}
