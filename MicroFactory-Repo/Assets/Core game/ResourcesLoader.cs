using RA.CommandConsole;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResourcesLoader
{
    public static bool allowBaseData = true;
    public static bool allowModData = false;

    private static List<IngredientData> ingredientDatas = new List<IngredientData>();
    private static List<RecipeData> recipeDatas = new List<RecipeData>();
    private static List<NodeData> nodeDatas = new List<NodeData>();
    private static List<EffectData> effectDatas = new List<EffectData>();
    private static List<TagData> tagDatas = new List<TagData>();
    // private static List<GameModeData> gameModeDatas = new List<GameModeData>();
    // private static List<TokensData> tokensDatas = new List<TokensData>(); // añadir ??

    // Ingredients
    public static int IngredientAmount() => ingredientDatas.Count;
    public static IngredientData GetIngredient(string name) => ingredientDatas.First(x => x.ingredientName.Equals(name));
    public static IngredientData[] GetIngredientByTag(string tag) => ingredientDatas.Where(x => x.tags.Contains(tag)).ToArray();

    // Recipes
    public static int RecipeAmount() => recipeDatas.Count;
    public static RecipeData GetRecipe(string name) => recipeDatas.First(x => x.name.Equals(name)); // name fix (!!!)

    // Nodes
    public static int NodeAmount() => nodeDatas.Count;
    public static NodeData GetNode(string name) => nodeDatas.First(x => x.nodeName.Equals(name));
    public static NodeData GetNode(int i) => nodeDatas[i];
    public static List<NodeData> GetNodes() => new List<NodeData>(nodeDatas);
    public static NodeData[] GetNodesByTag(string tag) => nodeDatas.Where(x => x.tags.Contains(tag)).ToArray();

    // Effects
    public static int EffectAmount() => effectDatas.Count;
    public static EffectData GetEffect(string name) => effectDatas.First(x => x.effectName.Equals(name));
    public static EffectData GetEffect(int i) => effectDatas[i];
    public static List<EffectData> GetEffects() => new List<EffectData>(effectDatas);

    // Tags
    public static int TagsAmount() => tagDatas.Count;
    public static TagData GetTag(string name) => tagDatas.First(x => x.value.Equals(name));
    public static TagData GetTag(int i) => tagDatas[i];
    public static List<TagData> GetTags() => new List<TagData>(tagDatas);

    // GameModes
    // IMPLEMENTAR
    // public static GameModeData GetGameMode(string name) => gameModeDatas.First(x => x.name.Equals(name)); 

    /// <summary>
    /// Load the data of the game elements.
    /// </summary>
    public static void LoadDataGame() // llamar al inciar la escena de juego!!!
    {
        LoadBaseGameData();

        if(allowModData)
        {
            LoadModData();
        }
    }

    private static void LoadBaseGameData()
    {
        // Load Ingredients
        ingredientDatas = Resources.LoadAll<IngredientData>("BaseGame/Ingredients").ToList();
        Debug.Log("<color=#70FB5F>[Node Engine, Resources]</color> Load <b>" + ingredientDatas.Count + "</b> ingredient resources.");

        // Load Recipes
        recipeDatas = Resources.LoadAll<RecipeData>("BaseGame/Recipes").ToList();
        Debug.Log("<color=#70FB5F>[Node Engine, Resources]</color> Load <b>" + recipeDatas.Count + "</b> recipe resources.");

        // Load Nodes
        nodeDatas = Resources.LoadAll<NodeData>("BaseGame/Nodes").ToList();
        Debug.Log("<color=#70FB5F>[Node Engine, Resources]</color> Load <b>" + nodeDatas.Count + "</b> node resources.");

        // Load Effects
        effectDatas = Resources.LoadAll<EffectData>("BaseGame/Effects").ToList();
        Debug.Log("<color=#70FB5F>[Node Engine, Resources]</color> Load <b>" + effectDatas.Count + "</b> effect resources.");

        // Load Tags
        tagDatas = Resources.LoadAll<TagData>("BaseGame/Tags").ToList();
        Debug.Log("<color=#70FB5F>[Node Engine, Resources]</color> Load <b>" + tagDatas.Count + "</b> tag resources.");

        // Load GameModes
        // IMPLEMENTAR
    }


    /// <summary>
    /// Take the information loaded by "ModLoader" and 
    /// create Data with that information.
    /// </summary>
    private static void LoadModData()
    {
        for (int j = 0; j < ModLoader.ModsCount; j++)
        {
            var mod = ModLoader.GetMod(j);

            // Load Ingredients
            var ingredients = new List<IngredientData>();
            for (int i = 0; i < mod.ingredientsInfo.Count; i++)
            {
                try
                {
                    var ingredient = IngredientData.CreateFromInfo(mod.ingredientsInfo[i]);
                    ingredients.Add(ingredient);
                }
                catch { }
            }
            ingredientDatas.Concat(ingredients);
            Debug.Log("<color=#A92DDF>[Node Engine, Mod Resources]</color> Load <b>" + ingredients.Count + "</b> ingredient resources.");

            // Load Recipes
            var recipes = new List<RecipeData>();
            for (int i = 0; i < mod.recipesInfo.Count; i++)
            {
                try
                {
                    var recipe = RecipeData.CreateFromInfo(mod.recipesInfo[i]);
                    recipes.Add(recipe);
                }
                catch { }
            }
            recipeDatas.Concat(recipes);
            Debug.Log("<color=#A92DDF>[Node Engine, Mod Resources]</color> Load <b>" + recipes.Count + "</b> recipe resources.");

            // Load Nodes
            var nodes = new List<NodeData>();
            for (int i = 0; i < mod.nodesInfo.Count; i++)
            {
                try
                {
                    var node = NodeData.CreateFromInfo(mod.nodesInfo[i]);
                    nodes.Add(node);
                }
                catch { }
            }
            nodeDatas.Concat(nodes);
            Debug.Log("<color=#A92DDF>[Node Engine, Mod Resources]</color> Load <b>" + nodes.Count + "</b> node resources.");

            // Load Effects
            // IMPLEMENTAR

            // Load GameModes
            // IMPLEMENTAR
        }
    }


    [Command("Help nodeNames", "show a list of loaded nodes.", "Help node names")]
    public static void GetNodesNames() 
    {
        Debug.Log(nodeDatas.Count);
        foreach (var node in nodeDatas)
        {
            var n = node.name;
            Commands.Log(n);
        }
    }

    [Command("Help effectNames", "show a list of loaded effects.", "Help effect names")]
    public static void GetEffectNames()  
    {
        effectDatas.ForEach(n => Commands.Log(n.name));
    }

    [Command("Help ingredientNames", "show a list of loaded ingredients.", "Help ingredient names")]
    public static void GetIngredientNames() 
    {
        ingredientDatas.ForEach(n => Commands.Log(n.name));
    }
}
