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

    public void SetSize(float dist)
    {
        background.size = border.size = new Vector2(dist * 5, border.size.y);
    }

    public void SetColor(Color body, Color border)
    {
        this.border.color = border;
        this.background.color = body;
    }

    
}
