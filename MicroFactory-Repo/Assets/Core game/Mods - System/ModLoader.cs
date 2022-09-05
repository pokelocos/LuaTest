using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System;

public static class ModLoader // change name to data loader ??
{
    private static List<Mod> loadedMods = new List<Mod>();

    public static int ModsCount { get { return loadedMods.Count; } }

    /// <summary>
    /// Returns the saved Image with the name given by parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Sprite GetImage(string name)
    {
        for (int i = 0; i < loadedMods.Count; i++)
        {
            Sprite sprite;
            if(loadedMods[i].images.TryGetValue(name,out sprite))
            {
                return sprite;
            }
        }
        return null;
    }

    /// <summary>
    /// Returns the mod saved at index given by parameters.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public static Mod GetMod(int i)
    {
        return loadedMods[i];
    }

    /// <summary>
    /// Returns the saved mod with the name given by parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Mod GetMod(string name)
    {
        return loadedMods.First(m => name.Equals(m.basicInfo.name));
    }

    /// <summary>
    /// This function is called when loading the game, Load mods.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
#if UNITY_EDITOR
        LoadModsFromProject();
#else
        LoadModsFromRoot();
#endif
    }

    /// <summary>
    /// Load the mods in the "Resource/Mods" path inside the project
    /// </summary>
    private static void LoadModsFromProject()
    {
        LoadMods(Application.dataPath +"/Resources/Mods");
    }

    /// <summary>
    /// Load the mods in the "Mods" path of the game installation folder.
    /// </summary>
    private static void LoadModsFromRoot()
    {
        var dataPath = Application.dataPath;
        var path = dataPath.Replace("/" + Application.productName +"_Data", "");
        LoadMods(path + "/Mods");
    }

    /// <summary>
    /// load the mods in the path "Mods" given by parameter.
    /// </summary>
    /// <param name="root"></param>
    private static void LoadMods(string root)
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(root);
        var directory = dir.GetDirectories();

        for (int i = 0; i < directory.Length; i++)
        {
            var mod = LoadXmlMod(directory[i]);
            loadedMods.Add(mod);
            try
            {
                
                Debug.Log("<color=#2283FF>[ModLoader]</color> <b>" + mod.basicInfo.name + "</b> mod has been loaded:\n" + mod.DebugInfo());
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
        mod.basicInfo.thumnail = imgs["thumnail.png"]; // <-- ojo , no se como mejorar esto, osea no deberia estar aqui pero no se no donde ni como ponerlo

        // load sounds
        // XXXXX IMPLEMENTAR XXXXX

        //Load Lua
        var luas = LoadRecursiveLUA(modRoot, new List<string>());
        luas.ForEach(l => LuaCore.LoadLuaCode(l));
        //var luaString = LuaCore.ImportLUA(modRoot.FullName + "\\main.lua");
        //LuaCore.LoadLuaCode(luaString);
        return mod;
    }

    private static List<string> LoadRecursiveLUA(DirectoryInfo dir,List<string> luas)
    {
        var files = dir.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].FullName.EndsWith(".lua"))
            {
                try
                {
                    string current = LuaCore.ImportLUA(files[i].FullName);
                    luas.Add(current);
                }
                catch { }
            }
        }

        var dirs = dir.GetDirectories();
        for (int i = 0; i < dirs.Length; i++)
        {
            LoadRecursiveLUA(dirs[i], luas);
        }
        return luas;
    }

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

        public List<string> luaCode; 

        internal string DebugInfo() 
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
