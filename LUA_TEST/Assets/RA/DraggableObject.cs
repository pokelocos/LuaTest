using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private bool _dragged = false;
    private bool _overObjet = false;

    private void LateUpdate()
    {
        if (!_overObjet && !_dragged)
            return;

        var input = 0; // leftClick
        if (Input.GetMouseButton(input))
        {
            _dragged = true;
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,10));
            //worldPos.z = 0;
            Debug.Log(worldPos);
            transform.position = Vector2.Lerp(transform.position, worldPos, speed * Time.deltaTime);
        }
        else
        {
            _dragged = false;
        }
    }

    private void OnMouseEnter()
    {
        _overObjet = true;
    }

    private void OnMouseExit()
    {
        _overObjet = false;
    }
}
