using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DataSystem;
using UnityEngine.Events;
using System;

public class ModView : MonoBehaviour
{
    [Header("Pref reference")]
    [SerializeField] private Image image;
    [SerializeField] private Text textName;
    [SerializeField] private Text textAuthor;
    [SerializeField] private Text textDescription;

    public void SetInfo(ModLoader.Mod mod)
    {
        var modInfo = mod.basicInfo;
        SetInfo(
            modInfo.thumnail,
            modInfo.name,
            modInfo.author,
            modInfo.version,
            modInfo.description
            );
    }

    private void SetInfo(Sprite sprite, string name, string author, string version, string description)
    {
        image.sprite = sprite;
        textName.text = name;
        textAuthor.text = author;
        textAuthor.text = version;
        textDescription.text = description;
    }

}

