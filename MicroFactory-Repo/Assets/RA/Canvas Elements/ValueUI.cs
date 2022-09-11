using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text numberText;

    public void SetValue(string value)
    {
        numberText.text = value;
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
