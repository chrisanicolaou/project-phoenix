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
                    Object.Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}