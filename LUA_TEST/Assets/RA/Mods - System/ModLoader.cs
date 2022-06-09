using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System;

public static class ModLoader 
{
    private static List<Mod> loadedMods = new List<Mod>();

    public static int ModsCount { get { return loadedMods.Count; } }

    public static Mod GetMod(int i)
    {
        return loadedMods[i];
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMods()
    {
#if UNITY_EDITOR
        LoadModsFromProject();
#else
        LoadModsFromRoot();
#endif
        //Debug.Log("[ModLoader]: ("+loadedMods.Count+") mod loaded.");
    
    }

    private static void LoadModsFromProject()
    {
        LoadMods(Application.dataPath +"/Resources/Mods");
    }

    private static void LoadModsFromRoot()
    {
        var dataPath = Application.dataPath;
        var path = dataPath.Replace("/" + Application.productName +"_Data", "");
        LoadMods(path + "/Mods");
    }

    private static void LoadMods(string root)
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(root);
        var directory = dir.GetDirectories();

        for (int i = 0; i < directory.Length; i++)
        {
            try
            {
                var mod = LoadXmlMod(directory[i]);
                loadedMods.Add(mod);
                Debug.Log("[ModLoader]:'" + mod.basicInfo.name + "' mod has been loaded successfully.\n" + mod.DebugInfo());
            }
            catch
            {
                Debug.LogError("[ModLoader Error]:There was an error loading mod in '" + directory[i].FullName + "' folder root.");
            }
        }
    }

    private static Mod LoadXmlMod(DirectoryInfo modRoot) // esto recorre toda la carpeta de mod O^(n) cantidad de vez y podria ser mas eficiente
    {
        Mod mod = new Mod();

        // load basic info
        mod.basicInfo = XmlSaveSystem.ImportXml<ModBasicInfo>(modRoot.FullName + "\\metadata.xml");

        // load ingredients
        var ingredients = LoadRecursiveXmlInfo(modRoot, new List<IngredientInfo>());
        mod.ingredientsInfo = ingredients;

        // load recipes
        var recipes = LoadRecursiveXmlInfo(modRoot, new List<RecipeInfo>());
        mod.recipesInfo = recipes;

        // load nodes
        var nodes = LoadRecursiveXmlInfo(modRoot, new List<NodeInfo>());
        mod.nodesInfo = nodes;

        // load effects
        var effects = LoadRecursiveXmlInfo(modRoot, new List<EffectInfo>());
        mod.effectsInfo = effects;

        // load effects
        var modes = LoadRecursiveXmlInfo(modRoot, new List<GameModeInfo>());
        mod.gameModesInfo = modes;

        // load Images
        Dictionary<string, Sprite> imgs = LoadRecursiveImgs(modRoot, new Dictionary<string, Sprite> ());
        mod.images = imgs;

        // load sounds
        //AudioClip clip = null;
        //var root = modRoot.FullName + "\\Content\\441499__matrixxx__rocket-01.wav";
        //SfxImporter.Load(modRoot.FullName + "\\Content\\441499__matrixxx__rocket-01.wav", clip);

        //Load Lua
        //Dictionary<string string> functions 
        var luaString = LuaCore.ImportLUA(modRoot.FullName + "\\main.lua");
        LuaCore.DoString(luaString);

        return mod;
    }

    // las clases "LoadRecursiveXXX" pueden ser 3 aciones diferentes y una unica funcion recorra las carpetas de manera recursiva
    private static Dictionary<string,AudioClip> LoadRecursiveSFX(DirectoryInfo dir, Dictionary<string,AudioClip> SFXs)
    {
        var files = dir.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].FullName.Contains(".wav") && !files[i].FullName.Contains(".meta"))
            {
                AudioClip clip = null;
                SfxImporter.Load(files[i].FullName, clip);
                SFXs.Add(files[i].Name,clip);
            }
        }

        var dirs = dir.GetDirectories();
        for (int i = 0; i < dirs.Length; i++)
        {
            LoadRecursiveSFX(dirs[i], SFXs);
        }
        return SFXs;
    }

    private static Dictionary<string, Sprite> LoadRecursiveImgs(DirectoryInfo dir, Dictionary<string, Sprite> imgs)
    {
        var files = dir.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].FullName.Contains(".png") && !files[i].FullName.Contains(".meta"))
            {
                Sprite img = IMG2Sprite.LoadNewSprite(files[i].FullName);
                imgs.Add(files[i].Name, img);
            }
        }

        var dirs = dir.GetDirectories();
        for (int i = 0; i < dirs.Length; i++)
        {
            LoadRecursiveImgs(dirs[i], imgs);
        }
        return imgs;
    }

    private static List<T> LoadRecursiveXmlInfo<T>(DirectoryInfo dir,List<T> info)
    {
        var files = dir.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].FullName.Contains(".xml") && !files[i].FullName.Contains(".meta"))
            {
                T current;
                try
                {
                    if (TryImportXml(files[i].FullName, out current))
                    {
                        info.Add(current);
                    }
                }
                catch { }
            }
        }

        var dirs = dir.GetDirectories();
        for (int i = 0; i < dirs.Length; i++)
        {
            LoadRecursiveXmlInfo(dirs[i], info);
        }
        return info;
    }

    private static bool TryImportXml<T>(string fullName,out T info)
    {
        info = XmlSaveSystem.ImportXml<T>(fullName);
        return (!info.Equals(default(T)));
    }

    public struct Mod
    {
        public ModBasicInfo basicInfo;
        public List<IngredientInfo> ingredientsInfo;
        public List<RecipeInfo> recipesInfo;
        public List<NodeInfo> nodesInfo;
        public List<EffectInfo> effectsInfo;
        public List<GameModeInfo> gameModesInfo;

        public Dictionary<string, Sprite> images;
        public Dictionary<string, AudioClip> audios;

        public List<string> luaCode; // esto no se si sea correcto, yo creo que no lo voy a llenar pero puede ser importante no se.

        internal string DebugInfo() // quitar cuando los sonidos esten implementados
        {
            return "Ingredient: " + ingredientsInfo?.Count + "\n" +
                "Recipes: " + recipesInfo?.Count +"\n" +
                "Nodes: " + nodesInfo?.Count +"\n" +
                "Effects: " + effectsInfo?.Count +"\n" +
                "GameModes: " + gameModesInfo?.Count +"\n"+
                "---------------------------\n" +
                "Images: " + images?.Count + "\n"+
                "Audios: " + audios?.Count + "\n";
        }
    }
}
