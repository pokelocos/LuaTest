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

[XmlRoot(ElementName = "IngredientInfo")]
public struct IngredientInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Color1")] public string color1;
    [XmlElement(ElementName = "Color2")] public string color2;
    [XmlElement(ElementName = "Value")] public string value;
    [XmlArray("Tags")]
    [XmlArrayItem("Item")]
    public string[] tags; // agregar
}

[XmlRoot(ElementName = "RecipeInfo")]
public struct RecipeInfo
{
    [XmlElement(ElementName = "WaitTime")] public float waitTime;
    [XmlArray("Input")]
    [XmlArrayItem("Item")] public List<string> inputs;
    [XmlArray("Output")]
    [XmlArrayItem("Item")] public List<string> outputs;
}

[XmlRoot(ElementName = "NodeInfo")]
public struct NodeInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Description")] public string description;
    [XmlElement(ElementName = "Icon")] public Sprite icon;
    [XmlElement(ElementName = "BackgroundColor")] public string backgroundColor;
    [XmlElement(ElementName = "IconColor")] public string iconColor;
    [XmlElement(ElementName = "TimerColor")] public string timerColor;
    [XmlElement(ElementName = "InputMax")] public int inputMax;
    [XmlElement(ElementName = "OutputMax")] public int outputMax;
}

[XmlRoot(ElementName = "EffectInfo")]
public struct EffectInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Description")] public string description;
    [XmlElement(ElementName = "Icon")] public Sprite icon;
    [XmlElement(ElementName = "BackgroundColor")] public string backgroundColor;
    [XmlElement(ElementName = "IconColor")] public string iconColor;
    [XmlElement(ElementName = "TimerColor")] public string timerColor;
    [XmlElement(ElementName = "LifeSpan")] public string lifeSpan;
    [XmlElement(ElementName = "isPermanent")] public bool isPermanent; // esto no e si ira al final por que no se si se quedara la mecanica, talves tenga que ser un atributo d na etiueta
    // lo que afecta no se si deberia ser tag aqui o directamente en Lua

}

[XmlRoot(ElementName = "GameModeInfo")]
public struct GameModeInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Description")] public string description;
    //[XmlElement(ElementName = "Thumnail")] public Sprite thumnail;
    [XmlElement(ElementName = "Mode")] public string mode; // "VANILLA", "ONLY_MODS", "BOTH"

    // no se si poner las reglas aqui o como funciones en lua
    // si las pongo aqui puedo crear un modo de juego que varie su dificultad cambiando parametros nomas
    // si la pongo en lua podre poner nuvas reglas de gamar perder
    // talvez pueda poner atributos que digan tipo "isVanilla = true/false" que ea para alterar parametros
    // y que la info dentro de las etiquetas sea
}


