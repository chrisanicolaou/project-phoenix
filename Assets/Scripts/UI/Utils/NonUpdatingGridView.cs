using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI.Utils
{
    public class NonUpdatingGridView : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _cellSize;
        
        [SerializeField]
        private Vector2 _spacing;
        
        [SerializeField]
        private int _columnCount;

        public void CreateGrid(Transform parent)
        {
            var xPos = Mathf.RoundToInt(_cellSize.x / 2);
            var yPos = Mathf.RoundToInt(-_cellSize.y / 2);
            for (var i = 0; i < parent.childCount; i++)
            {
                if (i != 0 && i % 6 == 0)
                {
                    yPos -= Mathf.RoundToInt(_cellSize.y + _spacing.y);
                    xPos = Mathf.RoundToInt(_cellSize.x / 2);
                }
                var child = (RectTransform)parent.GetChild(i);
                child.sizeDelta = _cellSize;
                child.anchoredPosition = new Vector2(xPos, yPos);
                xPos += Mathf.RoundToInt(_cellSize.x + _spacing.x);
            }
        }
    }
}