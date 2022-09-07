using KnifeGame.Util;
using System;
using UnityEngine;


namespace KnifeGame.Managers
{
    public static class OptionsManager
    {
        public static event Action<bool> OnSwipesEnabledChanged;
        public static event Action<float> OnSfxVolumeChanged;
        public static event Action<float> OnMusicVolumeChanged;

        private static float _sfxVolume;
        private static float _musicVolume;
        private static bool _swipesEnabled;

        public static float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                if (_sfxVolume != value)
                {
                    OnSfxVolumeChanged?.Invoke(value);
                }
                _sfxVolume = value;
                PlayerPrefs.SetFloat("sfxVolume", _sfxVolume);
            }
        }
        public static float MusicVolume
        {
            get => _musicVolume;
            set
            {
                if (_musicVolume != value)
                {
                    OnMusicVolumeChanged?.Invoke(value);
                }
                _musicVolume = value;
                PlayerPrefs.SetFloat("musicVolume", _musicVolume);
            }
        }
        public static bool SwipesEnabled
        {
            get => _swipesEnabled;
            set
            {
                if (_swipesEnabled != value)
                {
                    OnSwipesEnabledChanged?.Invoke(value);
                }
                _swipesEnabled = value;
                PlayerPrefs.SetInt("showSwipe", _swipesEnabled ? 1 : 0);
            }
        }

        static OptionsManager()
        {
            SfxVolume = PlayerPrefs.HasKey("sfxVolume") ? PlayerPrefs.GetFloat("sfxVolume") : 1f;
            MusicVolume = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1f;
            SwipesEnabled = PlayerPrefs.HasKey("showSwipe") ? (PlayerPrefs.GetInt("showSwipe") == 1) : true;
        }
    }
}
