using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundTranslator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private RawImage _image;
    private float _yCurrentUV;

    private void Start()
    {
        _image = GetComponent<RawImage>();
        _yCurrentUV = _image.uvRect.y;
    }

    private void Update()
    {
        if (_yCurrentUV > 1)
            _yCurrentUV %= 1;

        _yCurrentUV += Time.deltaTime * _speed;

        _image.uvRect = new Rect(_image.uvRect.x, _yCurrentUV, _image.uvRect.width, _image.uvRect.height); 
    }
}
