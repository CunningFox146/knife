using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace KnifeGame.UI
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _deselectedColor;
        [Space]
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;

        private Button _button;
        private Menu _menu;
        private Sequence _sequence;

        private void Awake()
        {
            _menu = GetComponentInParent<Menu>();
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() => _menu.SelectItem(this));
        }

        private void ChangeColor(Color color)
        {
            KillSequence();

            float duration = 0.5f;
            _sequence = DOTween.Sequence()
                .Append(_image.DOColor(color, duration))
                .Join(_text.DOColor(color, duration))
                .SetEase(Ease.OutSine);
        }

        public void SelectItem()
        {
            ChangeColor(_selectedColor);
        }

        public void DeselectItem()
        {
            ChangeColor(_deselectedColor);
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