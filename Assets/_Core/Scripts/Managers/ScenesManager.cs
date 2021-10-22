using KnifeGame.Util;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnifeGame.Managers
{
    public class ScenesManager : Singleton<ScenesManager>
    {
        [SerializeField] private GameModes _mode = GameModes.Classic;

        public GameModes Mode { get => _mode; private set => _mode = value; }
    }
}
