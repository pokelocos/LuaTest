using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer nodeIcon;
    [SerializeField] private SpriteRenderer fillBar;

    public void SetView(NodeData nodeData)
    {
        body.color = nodeData.bgColor;
        nodeIcon.sprite = nodeData.icon;
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

