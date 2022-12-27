using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Utils
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            var childCount = transform.childCount;
            if (childCount > 0)
            {
                for (int i = childCount - 1; i > -1; i--)
                {
                    var child = transform.GetChild(i);
                    child.SetParent(child.parent.parent);
                    Object.Destroy(child.gameObject);
                }
            }
        }
    }
}