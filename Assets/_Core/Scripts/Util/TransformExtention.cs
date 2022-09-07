using UnityEngine;

namespace KnifeGame.Util
{
    public static class TransformExtention
    {
        public static void SetLayerInChildren(this Transform parent, int layer)
        {
            parent.gameObject.layer = layer;

            for (int i = 0, count = parent.childCount; i < count; i++)
            {
                parent.GetChild(i).SetLayerInChildren(layer);
            }
        }
    }
}
