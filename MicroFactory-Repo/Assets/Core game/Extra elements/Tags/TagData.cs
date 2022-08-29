using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New tag data", menuName = "MicroFactory/Tag Data...")]
public class TagData : ScriptableObject, IRecipeable
{
    public string value;

    public static TagData CreateFromInfo(TagInfo info)
    {
        var inst = ScriptableObject.CreateInstance("Tag") as TagData;
        inst.value = info.value;

        return inst;
    }

    public static List<TagData> CreateFromInfo( TagGroupInfo infos)
    {
        var insts = new List<TagData>();
        foreach (var value in infos.values)
        {
            var inst = ScriptableObject.CreateInstance("Tag") as TagData;
            inst.value = value;
            insts.Add(inst);
        }

        return insts;
    }
}

[XmlRoot(ElementName = "Tag")]
public struct TagInfo
{
    [XmlElement(ElementName = "Value")] public string value;
}

[XmlRoot(ElementName = "TagGroup")]
public struct TagGroupInfo
{
    [XmlArray("Values")]
    [XmlArrayItem("Value")] public List<string> values;
}

public interface IRecipeable { }