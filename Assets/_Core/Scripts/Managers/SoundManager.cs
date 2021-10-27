using KnifeGame.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace KnifeGame.Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioMixer _mixer;

        public static AudioMixer Mixer => Inst._mixer;

        private void Start()
        {
            OptionsManager.OnMusicVolumeChanged += OnMusicVolumeChangedHandler;
            OptionsManager.OnSfxVolumeChanged += OnSfxVolumeChangedHandler;

            OnMusicVolumeChangedHandler(OptionsManager.MusicVolume);
            OnSfxVolumeChangedHandler(OptionsManager.SfxVolume);
        }

        private void OnDestroy()
        {
            OptionsManager.OnMusicVolumeChanged -= OnMusicVolumeChangedHandler;
            OptionsManager.OnSfxVolumeChanged -= OnSfxVolumeChangedHandler;
        }

        private void OnSfxVolumeChangedHandler(float volume) => SetSfxVolume(volume);
        private void OnMusicVolumeChangedHandler(float volune) => SetMusicVolume(volune);

        public static void SetSfxVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.001f, 1f); // Log(0) will cause an error value
            Mixer.SetFloat("SFXVolume", Mathf.Log(volume) * 20f);
        }

        public static void SetMusicVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0.001f, 1f); // Log(0) will cause an error value
            Mixer.SetFloat("MusicVolume", Mathf.Log(volume) * 20f);
        }
    }
}