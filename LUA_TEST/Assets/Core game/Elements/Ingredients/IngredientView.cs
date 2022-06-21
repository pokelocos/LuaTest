using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer border;

    public void SetView(ConnectionData data)
    {
        SetColor(data.bgColor, data.borderColor);
    }

    public void SetColor(Color body, Color border)
    {
        this.border.color = border;
        this.background.color = body;
    }
    public void SetPosition(Vector3 position)
    {
        this.transform.position = position + Vector3.zero;
    }
}
