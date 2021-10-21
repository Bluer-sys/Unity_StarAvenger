using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class BuffRender : MonoBehaviour
{
    private float _elapsedTime = 0;
    private float _duration;
    private  Image _image;

    private Tween tween;

    public Image Image => _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _elapsedTime = 0;
        _image.fillAmount = 1;

        tween = _image.DOFillAmount(0, _duration).SetEase(Ease.Linear);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _duration)
        {
            gameObject.SetActive(false);
        }
    }

    public void InitializeRender(Sprite image, float duration)
    {
        _image.sprite = image;
        _duration = duration;
        gameObject.SetActive(true);
    }

    public void ResetBuff()
    {
        if(tween != null)
            tween.Complete();

        _elapsedTime = 0;
        _image.fillAmount = 1;

       tween = _image.DOFillAmount(0, _duration).SetEase(Ease.Linear);
    }
}
