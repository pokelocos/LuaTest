using MoonSharp.Interpreter;
using RA.UtilMonobehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[MoonSharpUserData]
public class Node : MonoBehaviour
{
    public string Name = "Ricardo";
    public ClockTimer clock;
    public SpriteRenderer render;

    public delegate void NodeEvent(Node node);
    public event NodeEvent OnEnd;

    // Start is called before the first frame update
    void Start()
    {
        clock.OnEnd += (c) => Debug.Log("end");
        clock.OnEnd += (c) => OnEnd?.Invoke(this);
        //clock.OnEnd += (c) => OnClockEnd(this);
        clock.OnEnd += (c) =>
        {
            LuaCore.Script.Globals["Node"] = UserData.Create(this);
            LuaCore.DoFunction("OnStartGame");
            LuaCore.Script.Globals.Remove("Node");
        };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomColor()
    {
        var r = Random.Range(0.0f, 1.0f);
        var g = Random.Range(0.0f, 1.0f);
        var b = Random.Range(0.0f, 1.0f);
        render.color = new Color(r,g,b);
    }


    public void OnClockEnd(Node node) // solo poner cosas de lua aqui (?)
    {
        LuaCore.Script.Globals["Node"] = UserData.Create(this);

        //LuaCore.script.DoString("Debug('nodo:'.. Node.Name)");
        // ejecutar funcion de archivo lua
        //OnEnd?.Invoke(this);

        LuaCore.Script.Globals.Remove("Node");
    }

}

