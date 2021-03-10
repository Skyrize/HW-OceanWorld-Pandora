using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum AutomaticCellMode
{
    HEIGHT,
    WIDTH,
    HEIGHT_AND_WIDTH,
    HEIGHT_SQUARE,
    WIDTH_SQUARE,
}

[RequireComponent(typeof(GridLayoutGroup))]
public class AutomaticGridLayoutCell : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected Vector2 size = Vector2.zero;
    [SerializeField] protected AutomaticCellMode mode = AutomaticCellMode.HEIGHT;

    [Header("References")]
    [SerializeField] protected RectTransform rectTransform = null;
    [SerializeField] protected GridLayoutGroup layout = null;
    public void Resize()
    {
        if (rectTransform == null) {
            rectTransform = this.gameObject.GetComponent<RectTransform>();
        }
        if (layout == null) {
            layout = GetComponent<GridLayoutGroup>();
        } else {
            size = layout.cellSize;
            switch (mode)
            {
                case AutomaticCellMode.HEIGHT:
                    size.y = rectTransform.rect.height;
                break;
                case AutomaticCellMode.WIDTH:
                    size.x = rectTransform.rect.width;
                break;
                case AutomaticCellMode.HEIGHT_AND_WIDTH:
                    size.y = rectTransform.rect.height;
                    size.x = rectTransform.rect.width;
                break;
                case AutomaticCellMode.HEIGHT_SQUARE:
                    size.y = rectTransform.rect.height;
                    size.x = rectTransform.rect.height;
                break;
                case AutomaticCellMode.WIDTH_SQUARE:
                    size.x = rectTransform.rect.width;
                    size.y = rectTransform.rect.width;
                break;
            }
            layout.cellSize = size;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}
