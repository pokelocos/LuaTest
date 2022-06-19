using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe data", menuName = "MicroFactory/Recipe Data...")]
public class RecipeData : ScriptableObject
{
    [SerializeField] public float time; 

    [SerializeField] public List<IngredientData> inputIngredients = new List<IngredientData>();
    [SerializeField] public List<IngredientData> otuputingredients = new List<IngredientData>();

    public static RecipeData CreateFromInfo(RecipeInfo info)
    {
        var inst = ScriptableObject.CreateInstance("RecipeData") as RecipeData;
        inst.time = info.waitTime;

        for (int i = 0; i < info.inputs.Count; i++)
        {
            var ing = ResourcesLoader.GetIngredient(info.inputs[i]);
            inst.inputIngredients.Add(ing);
        }

        for (int i = 0; i < info.outputs.Count; i++)
        {
            var ing = ResourcesLoader.GetIngredient(info.outputs[i]);
            inst.inputIngredients.Add(ing);
        }

        return inst;
    }
}

[XmlRoot(ElementName = "RecipeInfo")]
public struct RecipeInfo // añadir name para tener una id de referencia ?
{
    [XmlElement(ElementName = "WaitTime")] public float waitTime;
    [XmlArray("Input")]
    [XmlArrayItem("Item")] public List<string> inputs;
    [XmlArray("Output")]
    [XmlArrayItem("Item")] public List<string> outputs;
}
