using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer fillBar;

    private readonly Vector3 targetHeight = new Vector3(0.55f, 0.55f, 0.55f);
    private readonly Vector3 _defaultIconScaleRatio = new Vector2(256, 256) / new Vector3(0.55f, 0.55f, 0.55f);

    public void Awake()
    {
        //_defaultIconScaleRatio = new Vector2(256,256) * new Vector3(0.55f, 0.55f, 0.55f);
    }

    public void SetView(NodeData nodeData)
    {
        var oldSize = icon.sprite.bounds.size.y;
        body.color = nodeData.bgColor;
        icon.sprite = nodeData.icon;
        var newSize = icon.sprite.bounds.size.y;
        icon.transform.localScale = (oldSize/newSize) * targetHeight;

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

