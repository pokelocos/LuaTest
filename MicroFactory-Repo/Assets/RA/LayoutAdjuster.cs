using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutAdjuster : MonoBehaviour
{
    public bool CalcOnAwake = false;
    public float spacing = 10f;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        if (CalcOnAwake)
            CalculateAdjust();
    }

    [ContextMenu("Adjust",false,0)]
    private void CalculateFromEditor()
    {
        rect = GetComponent<RectTransform>();
        CalculateAdjust();
    }

    public void CalculateAdjust()
    {
        var h = 0f;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = this.transform.GetChild(i);

            var adjust = child.GetComponent<LayoutAdjuster>();
            if (adjust != null)
                adjust.CalculateAdjust();

            var rect = child.GetComponent<RectTransform>();
            if(rect.gameObject.activeInHierarchy)
                h += rect.sizeDelta.y;
        }
        var size = (h + spacing);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, size);
    }
}
