using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New credit data", menuName = "RA/Credit Data...")]
public class CreditData : ScriptableObject
{
    public string lenguage;

    public Header header;
    public List<InfoGroup> infoGroup;
    public Footer footer;

    [Serializable]
    public struct Header
    {
        public string title;
        public Sprite image;
        public string name;
    }

    [Serializable]
    public struct Footer
    {
        public string text;
    }

    [Serializable]
    public struct InfoGroup
    {
        public string title;
        public int colummns;
        public List<string> names;
    }
}
