using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImageInMenu : MonoBehaviour
{
    [SerializeField] Image container;
    [SerializeField] List<Sprite> images;

    private void Awake()
    {
        int randomNum = Random.Range(0, images.Count);

        container.sprite = images[randomNum];
    }
}
