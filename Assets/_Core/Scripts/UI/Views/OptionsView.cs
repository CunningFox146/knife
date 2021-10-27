using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class OptionsView : AnimatedView
    {
        [SerializeField] private Slider _sfxVolume;
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Toggle _swipeToggle;

        private void Start()
        {
            _sfxVolume.value = OptionsManager.SfxVolume;
            _musicVolume.value = OptionsManager.MusicVolume;
            _swipeToggle.isOn = OptionsManager.SwipesEnabled;

            _sfxVolume.onValueChanged.AddListener(OnSfxVolumeChanged);
            _musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
            _swipeToggle.onValueChanged.AddListener(OnSwipeBoxChanged);
        }

        public void OnSwipeBoxChanged(bool isEnabled)
        {
            OptionsManager.SwipesEnabled = isEnabled;
        }

        public void OnSfxVolumeChanged(float value)
        {
            OptionsManager.SfxVolume = value;
        }
        public void OnMusicVolumeChanged(float value)
        {
            OptionsManager.MusicVolume = value;
        }
    }
}
