using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string ToTMProColor(this string inputStr, Color color)
        {
            var htmlColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{htmlColor}>{inputStr}</color>";
        }
    }
}