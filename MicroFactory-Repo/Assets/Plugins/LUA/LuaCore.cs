using MoonSharp.Interpreter;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public static class LuaCore
{
    private static Script _script;
    private static Dictionary<string, List<DynValue>> functions = new Dictionary<string, List<DynValue>>();

    /// <summary>
    /// Returns the single instance of the Script component belonging to "MoonSharp"
    /// </summary>
    public static Script Script 
    {
        get
        {
            return (_script != null)? _script : _script = new Script();
        }
    }


    /// <summary>
    /// It collects all the classes and functions that reference through the class
    /// attributes belonging to lua and "LuaCommand".
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        UserData.RegisterAssembly();
        CollectMetohdsFromScripts();
    }

    /// <summary>
    /// Open a file and return its content.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ImportLUA(string path)
    {
        using (var reader = new StreamReader(path))
        {
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }
    }

    /// <summary>
    /// It receives text, if it is a Lua function, stores it for use and marks it with its name.
    /// </summary>
    /// <param name="code"></param>
    public static void LoadLuaCode(string code)
    {
        var name = code.Split(' ').Last();
        DynValue ret = Script.DoString(code);

        switch(ret.Type)
        {
            case DataType.Function:
                if(!functions.ContainsKey(name))
                {
                    functions.Add(name, new List<DynValue>() { ret }); // << 
                }
                else
                {
                    List<DynValue> list;
                    functions.TryGetValue(name,out list);
                    list.Add(ret);
                }
                break;

            default:
                Debug.LogWarning("<color=#CFB150>[Lua Error]</color>: Content type not identified.");
                break;
        }
    }

    /// <summary>
    /// It receives identifier text, if it contains a group of functions 
    /// with said identifier, it executes them.
    /// </summary>
    /// <param name="key"></param>
    public static void DoFunction(string key)
    {
        List<DynValue> functions;
        if (LuaCore.functions.TryGetValue(key, out functions))
        {
            foreach (var func in functions)
            {
                var coroutine = Script.CreateCoroutine(func).Coroutine;
                while (true)
                {
                    coroutine.Resume();
                    if (coroutine.State == CoroutineState.Dead)
                        break;
                }
            }
        }
        else
        {
            Debug.LogError("<color=#FF0000>[Lua Error]</color>: '" + key + "' don't exist.");
        }
    }

    /// <summary>
    /// Collects the methods marked with the "LuaCommand" attribute.
    /// </summary>
    private static void CollectMetohdsFromScripts()
    {
        var methods = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .SelectMany(t => t.GetMethods())
                    .Where(m => m.GetCustomAttributes(typeof(LuaCommandAttribute), false).Length > 0)
                    .ToArray();

        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttributes(typeof(LuaCommandAttribute), false)[0] as LuaCommandAttribute;
            var types = method.GetParameters().Select(x => x.ParameterType);

            var action = method.CreateDelegate(Expression.GetDelegateType(types
            .Concat(new[] { method.ReturnType })
            .ToArray()));

            Script.Globals[attribute.Id] = action;
        }
    }

}