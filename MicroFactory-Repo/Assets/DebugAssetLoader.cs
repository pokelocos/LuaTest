using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugAssetLoader : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        GeneralInfo();
        //NodeInfo();
    }

    public void NodeInfo()
    {
        var msg = "";
        foreach (var node in ResourcesLoader.GetNodes())
        {
            msg += node.nodeName + "\n";
        }
        text.text = msg;
    }

    public void GeneralInfo()
    {
        var msg = "";
        msg += "Allow base: " + ResourcesLoader.allowBaseData + "\n";
        msg += "Allow mods: " + ResourcesLoader.allowModData + "\n";
        msg += "Ingredients: " + ResourcesLoader.IngredientAmount() + "\n";
        msg += "Recipes: " + ResourcesLoader.RecipeAmount() + "\n";
        msg += "Nodes: " + ResourcesLoader.NodeAmount() + "\n";
        text.text = msg;
    }

}
