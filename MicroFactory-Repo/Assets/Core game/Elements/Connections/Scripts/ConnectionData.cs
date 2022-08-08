using RA;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New connection data", menuName = "MicroFactory/Connection Data...")]
public class ConnectionData : ScriptableObject
{
    public List<string> tags;
    public Color bgColor = Color.gray;
    public Color borderColor = Color.grey;

    public float time = 1f;

    public static ConnectionData CreateFromInfo(ConnectionInfo info)
    {
        var inst = ScriptableObject.CreateInstance("ConnectionData") as ConnectionData;
        inst.time = info.waitTime;
        inst.bgColor = Commons.StrToColor(info.backgroundColor);
        inst.borderColor = Commons.StrToColor(info.borderColor);
        inst.tags = info.tags;

        return inst;
    }
}

[XmlRoot(ElementName = "ConnectionInfo")]
public struct ConnectionInfo
{
    [XmlElement(ElementName = "WaitTime")] public float waitTime;
    [XmlElement(ElementName = "BackgroundColor")] public string backgroundColor;
    [XmlElement(ElementName = "BorderColor")] public string borderColor;
    
    [XmlArray("Tags")]
    [XmlArrayItem("Tag")] public List<string> tags;
}