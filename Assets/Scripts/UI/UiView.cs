namespace DefaultNamespace.UI
{
    using UnityEngine;

    public interface IUiView
    {
        void SwitchMenuActive();
    }

    public class UiView : MonoBehaviour, IUiView
    {
        [SerializeField] GameObject _menu;

        
        public void SwitchMenuActive()   => _menu.SetActive( !_menu.activeSelf );
    }
}