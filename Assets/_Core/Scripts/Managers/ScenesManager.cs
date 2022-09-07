using KnifeGame.Util;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KnifeGame.Managers
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        public event Action OnSceneLoaded;
        public event Action<GameModes> OnGameModeChange;

        [SerializeField] private GameModes _mode = GameModes.Classic;

        public GameModes Mode
        {
            get => _mode;
            private set
            {
                if (_mode != value)
                {
                    OnGameModeChange?.Invoke(value);
                }
                _mode = value;
            }
        }

        public void SetGameMode(GameModes mode)
        {
            Mode = mode;
            StartCoroutine(LoadSceneCoroutine((int)mode));
        }

        private IEnumerator LoadSceneCoroutine(int scene)
        {
            var operation = SceneManager.LoadSceneAsync(scene);
            while (!operation.isDone)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame(); // So Start() is done running

            OnSceneLoaded?.Invoke();
        }
    }
}
