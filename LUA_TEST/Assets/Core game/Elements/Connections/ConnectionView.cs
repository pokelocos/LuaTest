using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer border;

    public void SetView(ConnectionData data)
    {
        SetColor(data.bgColor, data.borderColor);
    }

    private void SetSize(float dist)
    {
        background.size = border.size = new Vector2(dist * 5, border.size.y);
    }

    public void SetColor(Color body, Color border)
    {
        this.border.color = border;
        this.background.color = body;
    }

    public void SetPosition(Vector3 from, Vector3 to)
    {
        this.transform.position = from;
        this.transform.right = to - from;
        var dis = Vector3.Distance(from, to);
        SetSize(dis);

        //fade.transform.position = to;     // QUITAR PROBABLEMENTE 
        //input_icon.transform.position = from;
        //output_icon.transform.position = to;
        //fade.transform.localPosition -= Vector3.right * 8;
        //input_icon.transform.localPosition += Vector3.right * 8;
        //output_icon.transform.localPosition -= Vector3.right * 8;
    }
}
