using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using KnifeGame.Managers;

namespace KnifeGame.UI
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        private Button _button;
        private Menu _menu;
        private Sequence _sequence;

        private void Awake()
        {
            _menu = GetComponentInParent<Menu>();
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() => {
                _menu.SelectItem(this);
                SoundManager.PlayClick();
            });
        }

        private void ChangeColor(Color color)
        {
            KillSequence();

            float duration = 0.25f;
            _sequence = DOTween.Sequence()
                .Append(_image.DOColor(color, duration))
                .Join(_text.DOColor(color, duration))
                .SetEase(Ease.InOutSine);
        }

        public void SelectItem(Color color)
        {
            ChangeColor(color);
        }

        public void DeselectItem(Color color)
        {
            ChangeColor(color);
        }

        private void KillSequence()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }
        }
    }
}