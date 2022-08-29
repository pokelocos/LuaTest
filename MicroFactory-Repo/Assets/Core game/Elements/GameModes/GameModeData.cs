using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New gameMode data", menuName = "MicroFactory/GameMode Data...")]
public class GameModeData : ScriptableObject // cambiar nombre a "RuleData" o algo asi (?)
{
    public string nameGameMode;
    [TextArea] public string description;

    public int startMoney = 1000;
    public int maxPoint = 5;
    public bool allowMods = false; // (?)

    public List<StartNodesData> nodes = new List<StartNodesData>();

    public static GameModeData CreateFromInfo(GameModeInfo info)
    {
        var inst = ScriptableObject.CreateInstance("GameModeData") as GameModeData;
        inst.nameGameMode = info.name;
        inst.description = info.description;
        inst.startMoney = info.startMoney;

       
        for (int i = 0; i < info.startNodes.Count; i++)
        {
            var data = ResourcesLoader.GetNode(info.startNodes[i].name);
            var pos = new Vector2(info.startNodes[i].x, info.startNodes[i].y);
            inst.nodes.Add(new StartNodesData(data, pos));
        }
        return inst;
    }

    [System.Serializable]
    public struct StartNodesData
    {
        public NodeData nodeData;
        public Vector2 position;

        public StartNodesData(NodeData nodeData, Vector2 position)
        {
            this.nodeData = nodeData;
            this.position = position;
        }
    }
}

[XmlRoot(ElementName = "GameModeInfo")]
public struct GameModeInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Description")] public string description;
    [XmlElement(ElementName = "Money")] public int startMoney;
    [XmlElement(ElementName = "MaxPoint")] public int maxPoint;
    [XmlElement(ElementName = "AllowMods")] public bool allowMods;


    [XmlArray("StartNodes")]
    [XmlArrayItem("StartNodes")] public List<StartNodes> startNodes;
}

[XmlRoot(ElementName = "StartNodes")]
public struct StartNodes
{
    [XmlElement(ElementName = "NodeName")] public string name;
    [XmlElement(ElementName = "X")] public float x;
    [XmlElement(ElementName = "Y")] public float y;
}