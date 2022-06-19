using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer fillBar;

    private Vector3 _defaultIconScaleRatio;

    public void Awake()
    {
        _defaultIconScaleRatio = icon.sprite.rect.size * icon.transform.localScale;
    }

    public void SetView(NodeData nodeData)
    {
        body.color = nodeData.bgColor;
        icon.sprite = nodeData.icon;
        icon.transform.localScale = icon.sprite.rect.size / _defaultIconScaleRatio;
    }

    public void SetBodyColor(Color color)
    {
        body.color = color;
    }

    internal void SetBarAmount(float amount)
    {
        var radial = (1 - amount) * 360;
        fillBar.material.SetFloat("_Arc2", radial);
    }

    internal void SetBarColor(Color Color)
    {
        fillBar.color = Color;
    }
}

