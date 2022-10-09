namespace DefaultNamespace.UI
{
    using UnityEngine;
    using Zenject;
    
    public interface IUiView
    {
        void SwitchMenuActive();
        void ViewBuff(Sprite image, float duration);
        bool TryResetBuff(Sprite image);
    }

    public class UiView : MonoBehaviour, IUiView
    {
        [SerializeField] GameObject _menu;

        [Inject] IBuffsViewer _buffsViewer;
        
        
        public void SwitchMenuActive()                                  => _menu.SetActive( !_menu.activeSelf );

        public void ViewBuff(Sprite image, float duration)              => _buffsViewer.View( image, duration );

        public bool TryResetBuff(Sprite image)                          => _buffsViewer.TryResetView( image );
    }
}