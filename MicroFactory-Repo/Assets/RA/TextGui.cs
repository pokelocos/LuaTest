using RA.UtilMonobehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGui : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected Text valueText;

    public void SetValue(string value)
    {
        valueText.text = value;
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}