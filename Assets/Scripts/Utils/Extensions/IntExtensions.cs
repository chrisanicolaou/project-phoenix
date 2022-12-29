using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Utils.Extensions
{
    public static class IntExtensions
    {
        public static string ToTMProColor(this int inputStr, Color color)
        {
            var htmlColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{htmlColor}>{inputStr}</color>";
        }
    }
}