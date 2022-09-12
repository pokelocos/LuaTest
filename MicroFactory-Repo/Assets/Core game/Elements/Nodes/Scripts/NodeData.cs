using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using RA;
using System.Linq;

[CreateAssetMenu(fileName = "New node data",menuName = "MicroFactory/Node Data...")]
public class NodeData : ScriptableObject // atributos publico (???)
{
    public string nodeName;
    public string description;
    public int maintainCost;
    public Sprite icon;
    public List<string> tags = new List<string>();
    public List<RecipeData> recipes = new List<RecipeData>();

    public Color bgColor = Color.gray;
    public Color iconColor = Color.white;
    public Color timerColor = Color.white;

    private int inputMax;
    private int outputMax;

    public int MaxInput => inputMax;
    public int MaxOutput => outputMax;


    public void Start()
    {
        if (recipes.Count > 0)
        {
            Debug.Log("rep: " + recipes.Count);
            inputMax = recipes.Max(x => x.inputIngredients.Count);
            outputMax = recipes.Max(x => x.outputIngredients.Count);
        }
    }


    public static NodeData CreateFromInfo(NodeInfo info)
    {
        var inst = ScriptableObject.CreateInstance("NodeData") as NodeData;
        inst.nodeName = info.name;
        inst.description = info.description;
        inst.maintainCost = info.maintainCost;
        inst.icon = ModLoader.GetImage(info.iconName);
        inst.bgColor = Commons.StrToColor(info.backgroundColor);
        inst.iconColor = Commons.StrToColor(info.iconName);
        inst.timerColor = Commons.StrToColor(info.timerColor);

        for (int i = 0; i < info.recipes.Count; i++)
        {
            var recipe = ResourcesLoader.GetRecipe(info.recipes[i]);
            inst.recipes.Add(recipe);
        }

        inst.inputMax = inst.recipes.Max(x => x.inputIngredients.Count);
        inst.outputMax = inst.recipes.Max(x => x.outputIngredients.Count);

        inst.tags = info.tags;

        return inst;
    }
}

[XmlRoot(ElementName = "NodeInfo")]
public struct NodeInfo
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "Description")] public string description;
    [XmlElement(ElementName = "MaintainCost")] public int maintainCost;
    [XmlElement(ElementName = "Icon")] public string iconName;
    [XmlElement(ElementName = "BackgroundColor")] public string backgroundColor;
    [XmlElement(ElementName = "IconColor")] public string iconColor;
    [XmlElement(ElementName = "TimerColor")] public string timerColor;
    [XmlElement(ElementName = "InputMax")] public int inputMax; // quitar
    [XmlElement(ElementName = "OutputMax")] public int outputMax; // quitar

    [XmlArray("Recipes")]
    [XmlArrayItem("Recipe")] public List<string> recipes;

    [XmlArray("Tags")]
    [XmlArrayItem("Tag")] public List<string> tags;
}