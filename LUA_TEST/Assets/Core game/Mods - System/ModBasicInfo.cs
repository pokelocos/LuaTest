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


