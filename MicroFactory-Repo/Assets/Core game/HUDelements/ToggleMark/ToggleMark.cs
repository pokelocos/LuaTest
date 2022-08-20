using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMark : MonoBehaviour
{
    public Status enable;
    public Status disable;

    [Header("Pref reference")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image icon;
    [SerializeField] private Image background;

    public Action<bool> OnChange;

    public void OnValueChange()
    {
        var value = toggle.isOn;
        SetStatus(value ? enable : disable);
        OnChange?.Invoke(value);
    }

    public void SetValue(bool value)
    {
        toggle.isOn = value;
        SetStatus(value ? enable : disable);
        OnChange?.Invoke(value);
    }

    private void SetStatus(Status status)
    {
        icon.sprite = status.sprite;
        background.color = status.color;
    }

    [System.Serializable]
    public struct Status
    {
        public Color color;
        public Sprite sprite;
    }
}
