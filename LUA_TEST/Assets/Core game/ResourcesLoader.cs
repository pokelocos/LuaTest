using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResourcesLoader
{
    public static bool allowBaseData = true;
    public static bool alloModData = false;

    private static List<IngredientData> ingredientDatas = new List<IngredientData>();
    private static List<RecipeData> recipeDatas = new List<RecipeData>();
    private static List<NodeData> nodeDatas = new List<NodeData>();
    private static List<EffectData> effectDatas = new List<EffectData>();
    // private static List<GameModeData> GameModeDatas = new List<GameModeData>();

    public static int IngredientAmount() => ingredientDatas.Count;
    public static IngredientData GetIngredient(string name) => ingredientDatas.First(x => x.ingredientName.Equals(name));

    public static int RecipeAmount() => recipeDatas.Count;
    public static RecipeData GetRecipe(string name) => recipeDatas.First(x => x.name.Equals(name)); // name ??

    public static int NodeAmount() => nodeDatas.Count;
    public static NodeData GetNode(string name) => nodeDatas.First(x => x.name.Equals(name));

    //public static int EffectAmount() => EffectDatas.Count;
    //public static EffectData GetEffect(string name) => effectDatas.First(x => x.name.Equals(name));
    
    //public static GameModeData GetGameMode(string name) => gameModeDatas.First(x => x.name.Equals(name)); 

    /// <summary>
    /// Load the data of the game elements.
    /// </summary>
    public static void LoadDataGame() // llamar al inciar la escena de juego!!!
    {
        if(allowBaseData)
        {
            LoadBaseGameData();
        }

        if(alloModData)
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
        // IMPLEMENTAR

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

}
