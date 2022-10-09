namespace DefaultNamespace.UI
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;
    
    public interface IUiView
    {
        Button MainMenuButton                   { get; }
        Button RestartButton                    { get; }
        Button ExitButton                       { get; }
        Button ContinueButton                   { get; }
        Animator CompleteLevelAnimator          { get; }
        void SetMenuActive(bool state);
        void SwitchMenuActive();
        void ViewBuff(Sprite image, float duration);
        bool TryResetBuff(Sprite image);
        void SetCompleteLevelTextAlpha(float alpha);
        void PlayButtonClickSound();
        void SetAllButtonsInteractable(bool state);
        void SetGameOverTextActive(bool state);
    }

    public class UiView : MonoBehaviour, IUiView
    {
        [SerializeField] GameObject         _menu;
        [SerializeField] TextMeshProUGUI    _gameOverText;
        [SerializeField] TextMeshProUGUI    _completeLevelText;
        [SerializeField] Animator           _completeLevelAnimator;
        [SerializeField] Button             _mainMenuButton;
        [SerializeField] Button             _restartButton;
        [SerializeField] Button             _exitButton;
        [SerializeField] Button             _continueButton;
        [SerializeField] AudioSource        _buttonClickSound;

        [Inject] IBuffsViewer _buffsViewer;


        public Button MainMenuButton                                    => _mainMenuButton;
        
        public Button RestartButton                                     => _restartButton;
        
        public Button ExitButton                                        => _exitButton;
        
        public Button ContinueButton                                    => _continueButton;
        
        public Animator CompleteLevelAnimator                           => _completeLevelAnimator;
        
        public void SetMenuActive(bool state)                           => _menu.SetActive( state );

        public void SwitchMenuActive()                                  => _menu.SetActive( !_menu.activeSelf );

        public void ViewBuff(Sprite image, float duration)              => _buffsViewer.View( image, duration );

        public bool TryResetBuff(Sprite image)                          => _buffsViewer.TryResetView( image );
        
        public void SetCompleteLevelTextAlpha(float alpha)              => _completeLevelText.alpha = alpha;
        
        public void PlayButtonClickSound()                              => _buttonClickSound.Play();
        
        public void SetGameOverTextActive(bool state)                   => _gameOverText.gameObject.SetActive( state );

        public void SetAllButtonsInteractable(bool state)
        {
            _mainMenuButton.interactable    = state;
            _restartButton.interactable     = state;
            _exitButton.interactable        = state;
            _continueButton.interactable    = state;
        }
    }
}