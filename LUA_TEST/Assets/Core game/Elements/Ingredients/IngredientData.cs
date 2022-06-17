using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New ingredient data", menuName = "MicroFactory/Ingredient Data...")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public int value;
    public Sprite icon;
    public Color primaryColor = Color.gray;
    public Color secondaryColor = Color.grey;

    public  List<string> tags= new List<string>();
    public override bool Equals(object other)
    {
        var o = (IngredientData)other;
        if (o == null) 
            return false;

        return this.ingredientName == o.ingredientName;
    }

    public static void CreateFromInfo(IngredientInfo info)
    {
        var inst = ScriptableObject.CreateInstance("tempData_" + info.name) as IngredientData;
        inst.ingredientName = info.name;
        inst.value = info.value;
        inst.primaryColor =  RA.Commons.StrToColor(info.color1);
        inst.secondaryColor = RA.Commons.StrToColor(info.color2);

        inst.tags = info.tags.ToList();
    }
}

[XmlRoot(ElementName = "IngredientInfo")]
public struct IngredientInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Color1")] public string color1;
    [XmlElement(ElementName = "Color2")] public string color2;
    [XmlElement(ElementName = "Value")] public int value;
    [XmlArray("Tags")]
    [XmlArrayItem("Item")]
    public string[] tags; // agregar
}