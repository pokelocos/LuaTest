using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe data", menuName = "MicroFactory/Recipe Data...")]
public class RecipeData : ScriptableObject
{
    [SerializeField] public string recipeName;
    [SerializeField] public float time = 1;
    [SerializeField] public float productionProfit = 0;

    [SerializeField] public List<IngredientData> inputIngredients = new List<IngredientData>();
    [SerializeField] public List<IngredientData> outputIngredients = new List<IngredientData>();

    public static RecipeData CreateFromInfo(RecipeInfo info)
    {
        var inst = ScriptableObject.CreateInstance("RecipeData") as RecipeData;
        inst.recipeName = info.name;
        inst.time = info.waitTime;
        inst.productionProfit = info.profit;

        for (int i = 0; i < info.inputs.Count; i++)
        {
            var ing = ResourcesLoader.GetIngredient(info.inputs[i]);
            inst.inputIngredients.Add(ing);
        }

        for (int i = 0; i < info.outputs.Count; i++)
        {
            var ing = ResourcesLoader.GetIngredient(info.outputs[i]);
            inst.outputIngredients.Add(ing);
        }

        return inst;
    }
}

[XmlRoot(ElementName = "RecipeInfo")]
public struct RecipeInfo // añadir name para tener una id de referencia ?
{
    [XmlElement(ElementName = "Name")] public string name;
    [XmlElement(ElementName = "WaitTime")] public float waitTime;
    [XmlElement(ElementName = "Profit")] public float profit;
    [XmlArray("Input")]
    [XmlArrayItem("Item")] public List<string> inputs;
    [XmlArray("Output")]
    [XmlArrayItem("Item")] public List<string> outputs;
}
