using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MoneyBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _text;

    private Coroutine moneyChanger;

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int oldMoney,int newMoney)
    {
        if (moneyChanger != null)
        {
            _text.text = oldMoney.ToString();
            StopCoroutine(moneyChanger);
        }

        moneyChanger = StartCoroutine(MoneyChanger(oldMoney, newMoney));
    }

    private IEnumerator MoneyChanger(int oldMoney, int newMoney)
    {
        int currentMoney = oldMoney;
        int step = Mathf.Abs(newMoney - oldMoney) / 50 + 1;

        while(currentMoney < newMoney)
        {
            currentMoney += step;
            _text.text = currentMoney.ToString();
            yield return null;
        }
        _text.text = newMoney.ToString();

        moneyChanger = null;
    }
}
