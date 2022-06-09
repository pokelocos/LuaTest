using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Method)]
public class LuaCommandAttribute : Attribute
{
    private string id;

    public string Id { get { return id; } }

    public LuaCommandAttribute(string id)
    {
        this.id = id;
    }

}
