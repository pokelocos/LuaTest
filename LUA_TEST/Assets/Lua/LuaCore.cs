using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class LuaCore
{
    private static Script _script;
    private static Dictionary<string, List<DynValue>> functions = new Dictionary<string, List<DynValue>>();
    //private static Dictionary<string, MoonSharp.Interpreter.Coroutine> functions = new Dictionary<string, MoonSharp.Interpreter.Coroutine>();

    public static Script script {
        get
        {
            return (_script != null)? _script : _script = new Script();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        UserData.RegisterAssembly(); // << IMPORTANTE
        CollectMetohdsFromScripts();
    }

    public static string ImportLUA(string path)
    {
        using (var reader = new StreamReader(path))
        {
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }
    }


    // toma un string y lo transforma en una funcion
    public static void DoString(string code)
    {
        var name = code.Split(' ').Last();
        DynValue ret = script.DoString(code);

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
                Debug.Log("[Error] no se identifico el tipo del contenido");
                break;
        }
        
    }

    public static void DoFunction(string key)
    {
        //MoonSharp.Interpreter.Coroutine coroutine;
        List<DynValue> rets;
        if (functions.TryGetValue(key, out rets))
        {
            foreach (var ret in rets)
            {
                var coroutine = script.CreateCoroutine(ret).Coroutine;
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
            Debug.Log("[Lua Error]: '" + key + "' no existe"); //no se si este debug es correcto
        }
    }

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

            script.Globals[attribute.Id] = action;
        }
    }
}