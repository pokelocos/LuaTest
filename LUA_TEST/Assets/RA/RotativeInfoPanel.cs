using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA
{
    /// <summary>
    /// esta clase requiere que los pivotes tanto de el "infoPanel" como la "mask"
    /// </summary>
    public class RotativeInfoPanel : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private RectTransform infoPanel;
        [SerializeField] private RectTransform mask;

        private float end;

        void Start()
        {
            infoPanel.pivot = new Vector2(.5f, 1);
            infoPanel.anchorMin = new Vector2(.5f, 0);
            infoPanel.anchorMax = new Vector2(.5f, 0);
            mask.pivot = new Vector2(.5f, 1);

            end = -infoPanel.rect.y + -mask.rect.y;
        }

        void Update()
        {
            infoPanel.anchoredPosition += new Vector2(0, speed * Time.deltaTime);

            var y = infoPanel.anchoredPosition.y;
            if (end <= y)
            {
                Debug.Log(end +"<="+ y);
                infoPanel.anchoredPosition = new Vector2(0, 0);
            }
        }

    }
}