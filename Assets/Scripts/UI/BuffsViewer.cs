using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IBuffsViewer
{
    void View(Sprite image, float duration);
    bool TryResetView(Sprite image);
}

public class BuffsViewer : MonoBehaviour, IBuffsViewer
{
    [SerializeField] Transform _content;
    
    private readonly List<BuffRender> _imagesPool = new List<BuffRender>();

    private void Start()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            BuffRender childImage = _content.GetChild(i).GetComponent<BuffRender>();
            _imagesPool.Add(childImage);
            childImage.gameObject.SetActive(false);
        }
    }

    public void View(Sprite image, float duration)
    {
        BuffRender readyImagePlace = _imagesPool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        if(readyImagePlace != null)
        {
            readyImagePlace.InitializeRender(image, duration);
        }
    }

    public bool TryResetView(Sprite image)
    {
        foreach (var sameImage in _imagesPool)
        {
            if(sameImage.gameObject.activeSelf == true)
            {
                if(sameImage.Image.sprite.name == image.name)
                {
                    sameImage.ResetBuff();
                    return true;
                }
            }
        }
        return false;
    }
}
