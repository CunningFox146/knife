using KnifeGame.Util;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace KnifeGame.Managers
{
    public class ScenesManager : Singleton<ScenesManager>
    {
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
            SceneManager.LoadScene((int)mode);
        }
    }
}
