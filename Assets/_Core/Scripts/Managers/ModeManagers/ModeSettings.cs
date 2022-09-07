using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnifeGame.Managers.ModeManagers
{
    [CreateAssetMenu(fileName = "ModeSettings", menuName = "Scriptable Objects/ModeSettings")]
    public class ModeSettings : ScriptableObject
    {
        public bool resetOnHit = false;
        public GameObject hitEffect;
    }
}
