using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ModView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text textName;
    [SerializeField] private Text textAuthor;
    [SerializeField] private Text textVersion;
    [SerializeField] private Text textDescription;

    public void SetInfo(Sprite sprite, string name, string author, string version, string description)
    {
        image.sprite = sprite;
        textName.text = name;
        textAuthor.text = author;
        textAuthor.text = version;
        textDescription.text = description;
    }
}

