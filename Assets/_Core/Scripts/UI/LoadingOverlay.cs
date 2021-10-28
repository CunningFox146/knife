using DG.Tweening;
using KnifeGame.Managers;
using KnifeGame.Util;
using UnityEngine;

namespace KnifeGame.UI
{
    public class LoadingOverlay : Singleton<LoadingOverlay>
    {
        [SerializeField] RectTransform _knife;

        private CanvasGroup _canvasGroup;
        private Tween _knifeTween;
        private Tween _fadeTween;

        private void Start()
        {
            _knifeTween = _knife.DORotate(Vector3.back * 360f, 1.5f)
                .SetLoops(-1)
                .SetRelative(true)
                .Pause();

            _canvasGroup = GetComponent<CanvasGroup>();

            ScenesManager.Inst.OnGameModeChange += OnGameModeChangeHandler;
            ScenesManager.Inst.OnSceneLoaded += OnSceneLoadedHandler;

            DontDestroyOnLoad(gameObject);
        }

        public void OnDestroy()
        {
            ScenesManager.Inst.OnGameModeChange -= OnGameModeChangeHandler;
            ScenesManager.Inst.OnSceneLoaded -= OnSceneLoadedHandler;

            _knifeTween?.Kill();
            _fadeTween?.Kill();
        }

        private void OnSceneLoadedHandler()
        {
            _fadeTween?.Kill();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _fadeTween = _canvasGroup.DOFade(0f, 0.15f)
                .OnComplete(() => _knifeTween.Pause());
        }

        private void OnGameModeChangeHandler(GameModes mode)
        {
            _fadeTween?.Kill();
            _knifeTween.Play();

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _fadeTween = _canvasGroup.DOFade(1f, 0.15f);
        }
    }
}
