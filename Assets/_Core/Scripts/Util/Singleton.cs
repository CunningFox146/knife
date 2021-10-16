using UnityEngine;

namespace KnifeGame.Util
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected bool dontDestroyOnLoad = false;
        public static T Inst { get; private set; }

        protected virtual void Awake()
        {
            if (Inst != null)
            {
                Debug.Log($"Tried to create new instance of singleton {typeof(T)}");
                Destroy(gameObject);
                return;
            }
            Inst = this as T;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
