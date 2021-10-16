using System;
using System.Collections;
using UnityEngine;

namespace KnifeGame.Util
{
    public static class MonoBehaviourExtension
    {
        public static Coroutine DelayAction(this MonoBehaviour monoBeh, float delay, Action action)
        {
            IEnumerator ActionInTime()
            {
                yield return new WaitForSeconds(delay);
                action.Invoke();
            }
            return monoBeh.StartCoroutine(ActionInTime());
        }
    }
}
