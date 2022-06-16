using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA
{
    public class RotativeInfoPanel : MonoBehaviour // esto no deberia ser credit scene sino que deberia ser un componente que
    {
        [SerializeField] private Vector2 start, end;
        [SerializeField] private float speed;
        [SerializeField] private Transform infoPanel;

        private Rect infoRect;
        private Rect pivotRect;

        void Start()
        {
            infoRect = infoPanel.GetComponent<RectTransform>().rect;
            pivotRect = GetComponent<RectTransform>().rect;
        }

        void Update()
        {
            infoPanel.transform.position += new Vector3(0, speed * Time.deltaTime, 0);

            if (infoRect.yMin > (start + pivotRect.position).y)
            {
                infoPanel.transform.position = end + pivotRect.position;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var s = start + pivotRect.position;
            Gizmos.DrawLine(s + new Vector2(300, 0), s + new Vector2(-300, 0));
            Gizmos.DrawSphere(s + new Vector2(300, 0), 5f);
            Gizmos.DrawSphere(s + new Vector2(-300, 0), 5f);

            Gizmos.color = Color.red;
            var e = end + pivotRect.position;
            Gizmos.DrawLine(e + new Vector2(300, 0), e + new Vector2(-300, 0));
            Gizmos.DrawSphere(e + new Vector2(300, 0), 5f);
            Gizmos.DrawSphere(e + new Vector2(-300, 0), 5f);
        }
    }
}