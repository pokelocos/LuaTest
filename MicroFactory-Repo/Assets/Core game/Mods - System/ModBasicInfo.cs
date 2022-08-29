using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[XmlRoot(ElementName = "ModBasicInfo")]
public struct ModBasicInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Version")] public string version;
    [XmlElement(ElementName = "Author")] public string author;
    [XmlElement(ElementName = "Description")] public string description;
    [XmlElement(ElementName = "Thumnail")] public Sprite thumnail;
}
