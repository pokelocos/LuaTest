using RA;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


/// <summary>
/// Los effectos pueden cambiar:
/// "Velocidad de produccion de un nodo" Y
/// "Probabilidad de exito de un nodo" Y
/// "Velocidad de una conexion" N
/// "El Valor al que se vende un producto" N (multiplicativo y aditivo)
/// "El costo de manutencion de un Nodo" N
/// -> este clase deberia ten multiples hijos para todas las opciones o tener mucha info para que funcione para todo mmmm...
///     -> poner muchas clases significa tener mas Xmlroot lo que puede hacer mas dificil crearlos para los moders
///     -> tener todo en una sera mas dificil de implementar para el desarrollador
///     -> tener todo en uno hara que los moder se puedan confundir cambiando valores que no afectan a lo que deberia
/// </summary>
[CreateAssetMenu(fileName = "New Effect data", menuName = "MicroFactory/Effect Data...")]
public class EffectData : ScriptableObject
{
    public string effectName;
    public string description;
    public Sprite icon;
    public Color bgColor;
    public Color timerColor;
    public bool isPermanent;
    public float lifeSpan;

    public float speed; // multiply
    public float successProbability; // multiply

    public List<NodeData> WhiteListAffected; // si WL no es 0 son solo esos nodos
    public List<NodeData> BlackListAffected; // si BL no es 0 son todos los nodos menos esos

    public static EffectData CreateFromInfo(EffectInfo info)
    {
        var inst = ScriptableObject.CreateInstance("EffectData") as EffectData;
        inst.effectName = info.name;
        inst.description = info.description;
        inst.icon = ModLoader.GetImage(info.icon);
        inst.bgColor = Commons.StrToColor(info.backgroundColor);
        inst.timerColor = Commons.StrToColor(info.timerColor);
        inst.isPermanent = info.isPermanent;
        inst.lifeSpan = info.lifeSpan;

        return inst;
    }
}

[XmlRoot(ElementName = "EffectInfo")]
public struct EffectInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Description")] public string description;
    [XmlElement(ElementName = "Icon")] public string icon;
    [XmlElement(ElementName = "BackgroundColor")] public string backgroundColor;
    [XmlElement(ElementName = "IconColor")] public string iconColor;
    [XmlElement(ElementName = "TimerColor")] public string timerColor;
    [XmlElement(ElementName = "LifeSpan")] public float lifeSpan;
    [XmlElement(ElementName = "isPermanent")] public bool isPermanent; // esto no e si ira al final por que no se si se quedara la mecanica, talves tenga que ser un atributo d na etiueta

    [XmlArray("WhiteList")]
    [XmlArrayItem("Tag")] public List<string> WhiteListAffected;

    [XmlArray("BlackList")]
    [XmlArrayItem("Tag")] public List<string> BlackListAffected;
}
